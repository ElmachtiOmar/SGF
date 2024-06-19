
using Abp.Domain.Uow;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.ViewModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Web.Controllers
{
    public class FacturesController : Controller
    {
        private readonly IUnitOfWork<Facture> _unitOfWork;
        private readonly IUnitOfWork<Produit> _unitOfWorkProduit;
        private readonly IUnitOfWork<Client> _unitOfWorkClient;
        private readonly IUnitOfWork<LigneFacture> _unitOfWorkLF;
        private readonly IUnitOfWork<Payment> _unitOfWorkPayment;


        public FacturesController(IUnitOfWork<Facture> unitOfWork, IUnitOfWork<Produit> unitOfWorkProduit, IUnitOfWork<Client> unitOfWorkClient , IUnitOfWork<LigneFacture> unitOfWorkLF, IUnitOfWork<Payment> unitOfWorkPayment)
        {
            _unitOfWork = unitOfWork;
            _unitOfWorkProduit = unitOfWorkProduit;
            _unitOfWorkClient = unitOfWorkClient;
            _unitOfWorkLF = unitOfWorkLF;
            _unitOfWorkPayment = unitOfWorkPayment;
        }

        public ActionResult Index()
        {
            var factures = _unitOfWork.Entity.GetAllWithIncludesFacture().ToList();
            return View(factures);
            
        }

        public IActionResult Details(Guid id)
        {
            var facture = _unitOfWork.Entity.GetAllWithIncludesFacture().FirstOrDefault(f => f.Id == id);
            

            return View(facture);
        }
    

    public IUnitOfWork<Produit> Get_unitOfWorkProduit()
        {
            return _unitOfWorkProduit;
        }


        [HttpGet]
        public IActionResult Create()
        {

            var viewModel = new FactureViewModel
            {
                Clients = _unitOfWorkClient.Entity.GetAll().ToList(),
                Payments = _unitOfWorkPayment.Entity.GetAll().ToList(),
                LigneFactures = new List<LigneFactureViewModel>
            {
                new LigneFactureViewModel
                {
                    Produits = _unitOfWorkProduit.Entity.GetAll().ToList()
                }
            }
            };


            return View(viewModel);
        }

        // POST: Facture/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FactureViewModel viewModel)
        {

                var facture = new Facture
                {
                    Numero = viewModel.Numero,
                    NCheque = viewModel.NCheque,
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Client = _unitOfWorkClient.Entity.GetById(viewModel.ClientId),
                    Payment = _unitOfWorkPayment.Entity.GetById(viewModel.PaymentId),
                    LigneFactures = new List<LigneFacture>()
                };

                double netaPayerHT = 0;

                foreach (var lfViewModel in viewModel.LigneFactures)
                {
                    var produit = _unitOfWorkProduit.Entity.GetById(lfViewModel.ProduitId);
                    if (produit != null)
                    {
                        var totalHT = produit.PrixVenteHT * lfViewModel.Quantite;
                        var ligneFacture = new LigneFacture
                        {
                            Quantite = lfViewModel.Quantite,
                            Produit = produit,
                            TotalHT = totalHT,
                        };

                        facture.LigneFactures.Add(ligneFacture);
                        netaPayerHT += totalHT;
                        produit.QuantityStock -= lfViewModel.Quantite;
                    }
                }

                var payment = new Payment
                {
                ModeDePayment = viewModel.Payment.ModeDePayment,
                ModeDeLivraison = viewModel.Payment.ModeDeLivraison,
                PaymentState = viewModel.Payment.PaymentState,
                };
                
                
                
                facture.NetaPayerHT = netaPayerHT;
                facture.TotalTVA = netaPayerHT * 0.20; // Assuming a fixed 20% TVA rate
                facture.NetaPayerTTC = netaPayerHT + facture.TotalTVA;

                _unitOfWork.Entity.Insert(facture);
                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }

        public IActionResult Edit(Guid id)
        {
            var facture = _unitOfWork.Entity.GetById(id); 

            if (facture == null)
            {
                return NotFound();
            }

            return View(facture); 
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Facture facture)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Entity.Update(facture);
                return RedirectToAction("Index"); 
            }

            return View(facture); 
        }
    

    [HttpPost]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _unitOfWork.Entity.Delete(id);
            return Json(new { success = true });
        }
    }
}

