using BulkyBook.DataAccess.Repositry.IRepositry;
using BulkyBook.Models;
//using BulkyBook.Models.ViewModel;
//using BulkyBookWeb.Data;
using BulkyBook.DataAccess.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using BulkyBook.Models.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        //private readonly ICategoryRepositry _Db;
        private readonly IUnitOfWork _unitofwork;

        public CompanyController(IUnitOfWork unitofwork)
        {
            //_Db = db;
            _unitofwork = unitofwork;

        }

        public IActionResult Index()
        {
            //IEnumerable<Category> categories = _Db.GetAll();
            //IEnumerable<Product> products = _unitofwork.Product.GetAll();

            //return View(products);
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Company obj)
        {


            if (ModelState.IsValid)
            {
                //_Db.Add(obj);
                _unitofwork.Company.Add(obj);
                _unitofwork.Save();
                TempData["Success"] = "Created Successfuly";
                return RedirectToAction("Index");
            }



            return View(obj);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            //var details = _Db.Categories.Where(x => x.Id == id).FirstOrDefault();
            var details = _unitofwork.Company.GetFirstorDefault(x => x.Id == id);

            return View(details);
        }

        //Upsert means to update
        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            Company company = new();

            if (id == null || id == 0)
            {
                return View(company);
            }
            else
            {

                company = _unitofwork.Company.GetFirstorDefault(u => u.Id == id);
                return View(company);


            }



        }

        [HttpPost]
        public IActionResult Upsert(Company obj)
        {

            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    _unitofwork.Company.Add(obj);
                    TempData["Success"] = "Company Add Successfuly";
                }
                else
                {
                    _unitofwork.Company.Update(obj);
                    TempData["Success"] = "Company Updated Successfuly";
                }

            }
            _unitofwork.Save();
            return RedirectToAction("Index");

        }

        #region
        [HttpGet]
        public IActionResult GetAll()
        {
            var companiesList = _unitofwork.Company.GetAll();
            return Json(new { data = companiesList });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitofwork.Company.GetFirstorDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error While Deleting" });
            }
            _unitofwork.Company.Remove(obj);
            _unitofwork.Save();
            return Json(new { success = true, message = "Deleted Successfuly" });

        }
        #endregion


    }
}
