using HastaneAPP.WebUI.Models.Entity;
using HastaneAPP.WebUI.Services.AppDbService.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace HastaneAPP.WebUI.Controllers
{
    public class AdminController: Controller
    {
        private IOperasyonRepository _operatRepo;
        public AdminController(IOperasyonRepository operatRepo)
        {
            _operatRepo = operatRepo;
        }

        [HttpGet]
        public IActionResult CreateOperation()
        {
            var model = new Operasyonlar();
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateOperation(Operasyonlar model)
        {
            _operatRepo.Create(model);
            return RedirectToAction("ListOperation");
        }

        [HttpGet]
        public IActionResult ListOperation()
        {
            var models = _operatRepo.GetAll();

            return View(models);
        }



    }
}