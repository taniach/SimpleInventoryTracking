using System.Linq;
using SimpleInventoryTracking.Models;
using SimpleInventoryTracking.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SimpleInventoryTracking.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;

        public HomeController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var products = _productRepository.GetAllProducts().OrderBy(p => p.QuantityAvailable);

            var homeViewModel = new HomeViewModel()
            {
                Title = "Welcome to Simple Inventory Tracking",
                Products = products.ToList()
            };

            return View(homeViewModel);
        }

        public IActionResult Details(string productCode)
        {
            var product = _productRepository.GetProductByProductCode(productCode);

            if (product == null)
                return NotFound();

            return View(product);
        }
    }
}
