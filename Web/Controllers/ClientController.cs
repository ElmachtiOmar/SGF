using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ClientController : Controller
    {
        private readonly IUnitOfWork<Client> _unitOfWork;

        public ClientController(IUnitOfWork<Client> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            var client = _unitOfWork.Entity.GetAllWithIncludesClient().ToList();
            return View(client);
        }

        public ActionResult Details(Guid id)
        {
            var client = _unitOfWork.Entity.GetById(id);

            return View(client);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Client client)
        {
           
                var model = new Client
                {
                    Name = client.Name,
                    Code = client.Code,
                    Email = client.Email,
                    Phone = client.Phone,
                    Adress = new Adress
                    {
                        City = client.Adress.City,
                        Street = client.Adress.Street,
                        Numbre = client.Adress.Numbre
                    }
                };
                _unitOfWork.Entity.Insert(model);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));

        }


        public IActionResult Edit(Guid id)
        {
            var client = _unitOfWork.Entity.GetById(id);

            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Client client)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Entity.Update(client);
                return RedirectToAction("Index");
            }

            return View(client);
        }

        public ActionResult Delete(Guid id)
        {
            var client = _unitOfWork.Entity.GetById(id);

            return View(client);
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
