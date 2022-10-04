using BulkyBook.DataAccess.Repositry.IRepositry;
using BulkyBook.Models;
//using BulkyBookWeb.Data;
using BulkyBook.DataAccess.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        //private readonly ICategoryRepositry _Db;
        private readonly IUnitOfWork _unitofwork;
        public CoverTypeController(IUnitOfWork unitofwork)
        {
            //_Db = db;
            _unitofwork = unitofwork;
        }

        public IActionResult Index()
        {
            //IEnumerable<Category> categories = _Db.GetAll();
            IEnumerable<CoverType> covertypes = _unitofwork.CoverType.GetAll();

            return View(covertypes);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {
           

            if (ModelState.IsValid)
            {
                //_Db.Add(obj);
                _unitofwork.CoverType.Add(obj);
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
            var details = _unitofwork.CoverType.GetFirstorDefault(x => x.Id == id);

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
            var edit = _unitofwork.CoverType.GetFirstorDefault(x => x.Id == id);

            if (edit == null)
            {
                return NotFound();
            }

            return View(edit);
        }

        [HttpPost]
        public IActionResult Edit(CoverType ct)
        {
            _unitofwork.CoverType.Update(ct);
            _unitofwork.Save();
            TempData["Success"] = "Cover Type Updated Successfuly";
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            //var edit = _Db.Categories.Where(x => x.Id == id).FirstOrDefault();
            var covertypefromdbfirst = _unitofwork.CoverType.GetFirstorDefault(x => x.Id == id);
            if (covertypefromdbfirst == null)
            {
                return NotFound();
            }

            return View(covertypefromdbfirst);
        }

        [HttpPost]
        public IActionResult Delete(CoverType ct)
        {
            _unitofwork.CoverType.Remove(ct);
            _unitofwork.Save();
            TempData["Success"] = "Cover Type Deleted Successfuly";
            return RedirectToAction("Index");
        }


    }
}
