using BulkyBook.DataAccess.Repositry.IRepositry;
using BulkyBook.Models;
//using BulkyBookWeb.Data;
using BulkyBook.DataAccess.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        //private readonly ICategoryRepositry _Db;
        private readonly IUnitOfWork _unitofwork;
        public CategoryController(IUnitOfWork unitofwork)
        {
            //_Db = db;
            _unitofwork = unitofwork;
        }

        public IActionResult Index()
        {
            //IEnumerable<Category> categories = _Db.GetAll();
            IEnumerable<Category> categories = _unitofwork.Category.GetAll();

            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder)
            {
                ModelState.AddModelError("CustomError", "The Display Order Cannot Exactly same as Category Name");
            }

            if (ModelState.IsValid)
            {
                //_Db.Add(obj);
                _unitofwork.Category.Add(obj);
                _unitofwork.Save();
                TempData["Success"] = "Category Created Successfuly";
                return RedirectToAction("Index");
            }



            return View(obj);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            //var details = _Db.Categories.Where(x => x.Id == id).FirstOrDefault();
            var details = _unitofwork.Category.GetFirstorDefault(x => x.Id == id);

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
            var edit = _unitofwork.Category.GetFirstorDefault(x => x.Id == id);

            if (edit == null)
            {
                return NotFound();
            }

            return View(edit);
        }

        [HttpPost]
        public IActionResult Edit(Category ctrg)
        {
            _unitofwork.Category.Update(ctrg);
            _unitofwork.Save();
            TempData["Success"] = "Category Updated Successfuly";
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            //var edit = _Db.Categories.Where(x => x.Id == id).FirstOrDefault();
            var categoryfromdbfirst = _unitofwork.Category.GetFirstorDefault(x => x.Id == id);
            if (categoryfromdbfirst == null)
            {
                return NotFound();
            }

            return View(categoryfromdbfirst);
        }

        [HttpPost]
        public IActionResult Delete(Category ctrg)
        {
            _unitofwork.Category.Remove(ctrg);
            _unitofwork.Save();
            TempData["Success"] = "Category Deleted Successfuly";
            return RedirectToAction("Index");
        }


    }
}
