using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using Web.ViewModel;

namespace Web.Controllers
{
    public class ProduitController : Controller
    {
        private readonly IUnitOfWork<Produit> _unitOfWork;
        private readonly IUnitOfWork<Client> _unitOfWorkClient;
        private readonly IUnitOfWork<LigneFacture> _unitOfWorkLigneFacture;
        private readonly IUnitOfWork<Facture> _unitOfWorkFacture;
        private readonly IUnitOfWork<Payment> _unitOfWorkPayment;



        public ProduitController(IUnitOfWork<Produit> unitOfWork, IUnitOfWork<Client> unitOfWorkClient, IUnitOfWork<LigneFacture> unitOfWorkLigneFacture, IUnitOfWork<Facture> unitOfWorkFacture , IUnitOfWork<Payment> unitOfWorkPayment)
        {
            _unitOfWork = unitOfWork;
            _unitOfWorkClient = unitOfWorkClient;
            _unitOfWorkLigneFacture = unitOfWorkLigneFacture;
            _unitOfWorkFacture = unitOfWorkFacture;
            _unitOfWorkPayment = unitOfWorkPayment;
        }

        public ActionResult Index()
        {
            var produit = _unitOfWork.Entity.GetAll();
            return View(produit);
        }

        public IActionResult Details(Guid id)
        {
            var produit = _unitOfWork.Entity.GetById(id);

            if (produit == null)
            {
                return NotFound();
            }

            var mouvements = new List<MouvementProduitViewModel>();

            // Get all ligneFactures associated with this produit
            var ligneFactures = _unitOfWorkLigneFacture.Entity.GetAllWithIncludesLF().Where(lf => lf.Produit.Id == id).ToList();

            foreach (var ligneFacture in ligneFactures)
            {
                var facture = _unitOfWorkFacture.Entity.GetById(ligneFacture.Id);
               
                    var client = _unitOfWorkClient.Entity.GetById(facture.Client.Id);
                    var payment = _unitOfWorkPayment.Entity.GetById(facture.Payment.Id);

                    mouvements.Add(new MouvementProduitViewModel
                    {
                        FactureId = facture.Id,
                        FactureNumero = facture.Numero,
                        FactureDate = facture.Date,
                        Quantity = ligneFacture.Quantite,
                        TotalTTC = facture.NetaPayerTTC,
                        Client = client,
                        Payment = payment
                    });
                
            }

            var viewModel = new ProductViewModel
            {
                Id = produit.Id,
                Libelle = produit.Libelle,
                PrixAchatHT = produit.PrixAchatHT,
                PrixVenteHT = produit.PrixVenteHT,
                QuantityStock = produit.QuantityStock,
                Mouvements = mouvements
            };

            return View(viewModel);
        }


        //public ActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(Produit produit)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Entity.Insert(produit);
        //        _unitOfWork.Save();
        //        return RedirectToAction("Index");
        //    }
        //    return View(produit);
        //}

        public IActionResult Edit(Guid id)
        {
            var produit = _unitOfWork.Entity.GetById(id);

            if (produit == null)
            {
                return NotFound();
            }

            return View(produit);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Produit produit)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Entity.Update(produit);
                return RedirectToAction("Index");
            }

            return View(produit);
        }

        public ActionResult Delete(Guid id)
        {
            var produit = _unitOfWork.Entity.GetById(id);

            return View(produit);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _unitOfWork.Entity.Delete(id);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
