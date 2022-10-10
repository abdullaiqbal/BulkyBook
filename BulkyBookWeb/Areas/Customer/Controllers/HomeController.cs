//using BulkyBook.Models;
using BulkyBook.DataAccess.Repositry.IRepositry;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitofwork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public HomeController(IUnitOfWork unitofwork, IWebHostEnvironment hostEnvironment, ILogger<HomeController> logger)
        {
            _unitofwork = unitofwork;
            _hostEnvironment = hostEnvironment;
            _logger = logger;
        }

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitofwork.Product.GetAll(includeProperties: "Category,CoverType");
            return View(productList);
        }

        public IActionResult Details(int? id)
        {
            ShoppingCart cartObj = new()
            {
                Count = 1,
                Product = _unitofwork.Product.GetFirstorDefault(u => u.Id == id, includeProperties: "Category,CoverType")

            };
            
            return View(cartObj);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}