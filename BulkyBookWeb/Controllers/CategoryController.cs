using BulkyBook.Models;
using BulkyBookWeb.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _Db;
        public CategoryController(ApplicationDbContext db)
        {
            _Db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categories = _Db.Categories.ToList();

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
                _Db.Categories.Add(obj);
                _Db.SaveChanges();
                TempData["Success"] = "Category Created Successfuly";
                return RedirectToAction("Index");
            }

           

            return View(obj);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
           var details = _Db.Categories.Where(x => x.Id == id).FirstOrDefault();
       
            return View(details);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var edit = _Db.Categories.Where(x => x.Name == "id").FirstOrDefault();

            if(edit == null)
            {
                return NotFound();
            }

            return View(edit);
        }

        [HttpPost]
        public IActionResult Edit(Category ctrg)
        {
            _Db.Categories.Update(ctrg);
            _Db.SaveChanges();
            TempData["Success"] = "Category Updated Successfuly";
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
           var edit = _Db.Categories.Where(x => x.Id == id).FirstOrDefault();

           return View(edit);
        }

        [HttpPost]
        public IActionResult Delete(Category ctrg)
        {
            _Db.Categories.Remove(ctrg);
            _Db.SaveChanges();
            TempData["Success"] = "Category Deleted Successfuly";
            return RedirectToAction("Index");
        }


    }
}
