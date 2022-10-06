using BulkyBook.DataAccess.Repositry.IRepositry;
using BulkyBook.Models;
//using BulkyBook.Models.ViewModel;
//using BulkyBookWeb.Data;
using BulkyBook.DataAccess.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using BulkyBook.Models.ViewModels;


namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        //private readonly ICategoryRepositry _Db;
        private readonly IUnitOfWork _unitofwork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductController(IUnitOfWork unitofwork, IWebHostEnvironment hostEnvironment)
        {
            //_Db = db;
            _unitofwork = unitofwork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            //IEnumerable<Category> categories = _Db.GetAll();
            IEnumerable<Product> products = _unitofwork.Product.GetAll();

            return View(products);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product obj)
        {


            if (ModelState.IsValid)
            {
                //_Db.Add(obj);
                _unitofwork.Product.Add(obj);
                _unitofwork.Save();
                TempData["Success"] = "The Type of Cover Created Successfuly";
                return RedirectToAction("Index");
            }



            return View(obj);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            //var details = _Db.Categories.Where(x => x.Id == id).FirstOrDefault();
            var details = _unitofwork.Product.GetFirstorDefault(x => x.Id == id);

            return View(details);
        }

        //Upsert means to update
        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            ProductVM productVm = new();
            productVm.CategoryList = _unitofwork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            productVm.CoverTypeList = _unitofwork.CoverType.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            //ProductVM productVM = new()
            //{
            //    Product = new(),
            //    //CategoryList = _unitofwork.Category.GetAll().Select(
            //    //u => new SelectListItem
            //    //{
            //    //    Text = u.Name,
            //    //    Value = u.Id.ToString()
            //    //}),
            //    CategoryList = _unitofwork.Category.GetAll().Select(
            //        i => new SelectListItem
            //        {
            //            Text = i.Name,
            //            Value = i.Id.ToString()
            //        }),
            //    CoverTypeList = (IEnumerable<CoverType>)_unitofwork.CoverType.GetAll().Select(
            //    u => new SelectListItem
            //    {
            //        Text = u.Name,
            //        Value = u.Id.ToString()
            //    })

            //};
            //Product product = new();
            //IEnumerable<SelectListItem> categoryList = _unitofwork.Category.GetAll().Select(
            //    u => new SelectListItem
            //    {
            //        Text = u.Name,
            //        Value = u.Id.ToString()
            //    });
            //IEnumerable<SelectListItem> covertypeList = _unitofwork.CoverType.GetAll().Select(
            //   u => new SelectListItem
            //   {
            //       Text = u.Name,
            //       Value = u.Id.ToString()
            //   });
            if (id == null ||id==0)
            {
                //Create the Product
                //return NotFound();
                //ViewBag.categoryList = categoryList;
                //ViewBag.covertypeList = covertypeList;
                return View(productVm);
            }
            else
            {
                //Update the product

            }
            //var edit = _Db.GetFirstorDefault(x => x.Name == "id");
            //var edit = _unitofwork.Product.GetFirstorDefault(x => x.Id == id);

            //if (edit == null)
            //{
            //    return NotFound();
            //}

            //return View(edit);
            
            return View(productVm);
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM p, IFormFile? file)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            
            if(file != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath,@"Images\Products");
                var extension = Path.GetExtension(file.FileName);
                using (var filestreams= new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(filestreams);
                }
                p.Product.ImageUrl= @"\Images\Products" + fileName + extension;
            }
            _unitofwork.Product.Add(p.Product);
            _unitofwork.Save();
            TempData["Success"] = "Product Created Successfuly";
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            //var edit = _Db.Categories.Where(x => x.Id == id).FirstOrDefault();
            var productfromdbfirst = _unitofwork.Product.GetFirstorDefault(x => x.Id == id);
            if (productfromdbfirst == null)
            {
                return NotFound();
            }

            return View(productfromdbfirst);
        }

        [HttpPost]
        public IActionResult Delete(Product p)
        {
            _unitofwork.Product.Remove(p);
            _unitofwork.Save();
            TempData["Success"] = "Cover Type Deleted Successfuly";
            return RedirectToAction("Index");
        }


    }
}
