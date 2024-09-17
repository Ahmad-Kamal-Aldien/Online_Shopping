using Microsoft.AspNetCore.Mvc;
using Shopping.DataAccessLayer.Data;
using Shopping.DataAccessLayer.Repositorys.IRepository;
using Shopping.Entites.Model;
using System.Security.Claims;

namespace Shopping.Web.Areas.Admin.Controllers
{
  
  
    [Area("Admin")]
    public class CategoryController : Controller
    {
      


        IUnitOfWork UnitOfWork;
        public CategoryController(IUnitOfWork _UnitOfWork)
        {
            UnitOfWork = _UnitOfWork;
        }
        [HttpGet]
        public IActionResult Index()
        {
            ;
            return View("index", UnitOfWork.category.Get());
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Add(Category cat)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.category.Add(cat);
                UnitOfWork.complete();
                TempData["Add"] = "Data Add Success";
                return RedirectToAction("Index");
            }
            return View(cat);
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            Category category = UnitOfWork.category.GetFirst(c => c.Id == id);
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Update(Category category)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.category.Update(category);
                UnitOfWork.complete();
                TempData["Edit"] = "Data Updated Success";
                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
            }

        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Category cat = UnitOfWork.category.GetFirst(c => c.Id == id);


            return View(cat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult DeleteOK(int id)
        {

            Category cat = UnitOfWork.category.GetFirst(c => c.Id == id);
            UnitOfWork.category.Delete(cat);
            UnitOfWork.complete();
            TempData["Deleted"] = "Data Deleted ";
            return RedirectToAction("Index");
        }

      

    }
}
