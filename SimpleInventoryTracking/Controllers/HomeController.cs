using System.Linq;
using SimpleInventoryTracking.Models;
using SimpleInventoryTracking.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SimpleInventoryTracking.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;
        private IAuthorizationService _authorizationService { get; }
        private UserManager<IdentityUser> _userManager { get; }

        public HomeController(
            IProductRepository productRepository,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
        {
            _productRepository = productRepository;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var currentUserId = _userManager.GetUserId(User);

            var products = _productRepository.GetAllProducts(currentUserId)
                .OrderBy(p => p.QuantityAvailable);

            var homeViewModel = new HomeViewModel()
            {
                Title = "Welcome to Simple Inventory Tracking",
                Products = products.ToList()
            };

            return View(homeViewModel);
        }
    }
}
