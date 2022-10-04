using BulkyBook.DataAccess.Repositry.IRepositry;
using BulkyBook.Models;
//using BulkyBookWeb.Data;
using BulkyBook.DataAccess.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        //private readonly ICategoryRepositry _Db;
        private readonly IUnitOfWork _unitofwork;
        public ProductController(IUnitOfWork unitofwork)
        {
            //_Db = db;
            _unitofwork = unitofwork;
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
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var edit = _Db.GetFirstorDefault(x => x.Name == "id");
            var edit = _unitofwork.Product.GetFirstorDefault(x => x.Id == id);

            if (edit == null)
            {
                return NotFound();
            }

            return View(edit);
        }

        [HttpPost]
        public IActionResult Edit(Product p)
        {
            _unitofwork.Product.Update(p);
            _unitofwork.Save();
            TempData["Success"] = "Cover Type Updated Successfuly";
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
