using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shopping.DataAccessLayer.Repositorys.IRepository;
using Shopping.Entites.Model;
using Shopping.Entites.Model.ViewModels;
using X.PagedList;

namespace Shopping.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        IUnitOfWork UnitOfWork;
        IWebHostEnvironment webHostEnvironment;
        public ProductController(IUnitOfWork _UnitOfWork, IWebHostEnvironment _webHostEnvironment)
        {
            UnitOfWork = _UnitOfWork;
            webHostEnvironment = _webHostEnvironment;
        }
        [HttpGet]
        public IActionResult Index()
        {
           

            return View(UnitOfWork.product.Get());
        }
        [HttpGet]
        public IActionResult Add()
        {
            ProductCategoryViewModel pVM = new ProductCategoryViewModel()
            {
                Product = new Product(),
                CategoryList = UnitOfWork.category.Get().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };

            return View(pVM);
        }

        [HttpPost]
        public IActionResult Add(ProductCategoryViewModel productVM, IFormFile formFile)
        {
            if (ModelState.IsValid)
            {
                string Rootpath = webHostEnvironment.WebRootPath;
                if (formFile != null)
                {
                    string filename = Guid.NewGuid().ToString();
                    var Upload = Path.Combine(Rootpath, @"Images/Products");
                    var exe = Path.GetExtension(formFile.FileName);
                    using (var filestream = new FileStream(Path.Combine(Upload, filename + exe), FileMode.Create))
                    {
                        formFile.CopyTo(filestream);
                    }
                    productVM.Product.Image = @"Images\Products\" + filename + exe;
                }
                UnitOfWork.product.Add(productVM.Product);
                UnitOfWork.complete();
                TempData["Add"] = "Data Add Success";
                return RedirectToAction("Index");
            }


            return View(productVM.Product);
        }

        public IActionResult Details(int id)
        {
            Product pro = UnitOfWork.product.GetFirst(x => x.Id == id, include: "Category");
            return View(pro);
        }
        [HttpGet]
        public IActionResult GetByAjax()
        {
            var categories = UnitOfWork.product.Get(include: "Category");


            return Json(new { data = categories });
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ProductCategoryViewModel pVM = new ProductCategoryViewModel()
            {
                //The Same Object Product
                Product = UnitOfWork.product.GetFirst(x => x.Id == id),
                CategoryList = UnitOfWork.category.Get().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };

            return View(pVM);
        }
        [HttpPost]
        public IActionResult Update(ProductCategoryViewModel productVM, IFormFile? formFile)
        {
            if (ModelState.IsValid)
            {
                string Rootpath = webHostEnvironment.WebRootPath;
                if (formFile != null)
                {
                    string filename = Guid.NewGuid().ToString();
                    var Upload = Path.Combine(Rootpath, @"Images/Products");
                    var exe = Path.GetExtension(formFile.FileName);

                    //I Want To Delete Old Image

                    if (productVM.Product.Image != null)
                    {
                        var oldImage = Path.Combine(Rootpath, productVM.Product.Image.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImage))
                        {
                            System.IO.File.Delete(oldImage);
                        }
                    }

                    using (var filestream = new FileStream(Path.Combine(Upload, filename + exe), FileMode.Create))
                    {
                        formFile.CopyTo(filestream);
                    }
                    productVM.Product.Image = @"Images\Products\" + filename + exe;
                }
                UnitOfWork.product.Update(productVM.Product);
                UnitOfWork.complete();
                TempData["Edit"] = "Data Updated Success";
                return RedirectToAction("Index");
            }


            return View(productVM.Product);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            //I want Delete By Ajax And Delete Image
            Product pro = UnitOfWork.product.GetFirst(x => x.Id == id);

            if (pro == null)
            {
                return Json(new { success = false, message = "Error In Deleted" });
            }
            UnitOfWork.product.Delete(pro);
            if (pro.Image != null)
            {
                var oldImage = Path.Combine(webHostEnvironment.WebRootPath, pro.Image.TrimStart('\\'));
                if (System.IO.File.Exists(oldImage))
                {
                    System.IO.File.Delete(oldImage);
                }
            }




            UnitOfWork.complete();
            return Json(new { success = true, message = "Deleted Success OK" });
        }
    }
}
