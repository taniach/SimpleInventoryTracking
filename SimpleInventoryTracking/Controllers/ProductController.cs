using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleInventoryTracking.Models;
using SimpleInventoryTracking.ViewModels;
using System.Threading.Tasks;

namespace SimpleInventoryTracking.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ITransactionRepository _transactionRepository;
        private IAuthorizationService _authorizationService { get; }
        private UserManager<IdentityUser> _userManager { get; }

        public ProductController(
            IProductRepository productRepository,
            ITransactionRepository transactionRepository,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
        {
            _productRepository = productRepository;
            _transactionRepository = transactionRepository;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            if (ModelState.IsValid)
            {
                product.OwnerID = _userManager.GetUserId(User);

                var isAuthorized = await _authorizationService.AuthorizeAsync(
                                                User, product,
                                                new OperationAuthorizationRequirement
                                                { Name = "Create" });
                if (!isAuthorized.Succeeded)
                {
                    return new ChallengeResult();
                }


                _productRepository.AddProduct(product);
                return RedirectToAction("Index", "Home");
            }
            return View(product);
        }

        public IActionResult Delete(int id)
        {
            var product = _productRepository.GetProductByProductCode(
                id, _userManager.GetUserId(User));

            _productRepository.DeleteProduct(id);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Purchase(int id)
        {
            var product = _productRepository.GetProductByProductCode(
                id, _userManager.GetUserId(User));

            PurchaseViewModel purchaseViewModel = new PurchaseViewModel()
            {
                Id = id,
                ProductCode = product.ProductCode,
                Name = product.Name,
                Size = product.Size
            };

            return View(purchaseViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Purchase(PurchaseViewModel purchaseViewModel)
        {
            if (ModelState.IsValid)
            {
                var product = _productRepository.GetProductByProductCode(
                    purchaseViewModel.Id, _userManager.GetUserId(User));

                var isAuthorized = await _authorizationService.AuthorizeAsync(
                                                User, product,
                                                new OperationAuthorizationRequirement
                                                { Name = "Update" });
                if (!isAuthorized.Succeeded)
                {
                    return new ChallengeResult();
                }

                product.Purchase(purchaseViewModel.Quantity);
                _productRepository.UpdateProduct(product);

                _transactionRepository.AddTransaction(new Transaction() {
                    ProductId = product.Id,
                    ProductCode = product.ProductCode,
                    Name = product.Name,
                    Size = product.Size,
                    DateOfTransaction = purchaseViewModel.DatePurchased,
                    TypeOfTransaction = "Purchase",
                    Quantity = purchaseViewModel.Quantity,
                    Description = purchaseViewModel.Description,
                    OwnerID = _userManager.GetUserId(User)
                });

                return RedirectToAction("Index", "Home");
            }
            return View(purchaseViewModel);
        }

        public IActionResult Use(int id)
        {
            var product = _productRepository.GetProductByProductCode(
                id, _userManager.GetUserId(User));

            UseViewModel useViewModel = new UseViewModel()
            {
                Id = id,
                ProductCode = product.ProductCode,
                Name = product.Name,
                Size = product.Size
            };

            return View(useViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Use(UseViewModel useViewModel)
        {
            if (ModelState.IsValid)
            {
                var product = _productRepository.GetProductByProductCode(
                    useViewModel.Id, _userManager.GetUserId(User));

                if (product.Use(useViewModel.Quantity))
                {
                    var isAuthorized = await _authorizationService.AuthorizeAsync(
                                                User, product,
                                                new OperationAuthorizationRequirement
                                                { Name = "Update" });
                    if (!isAuthorized.Succeeded)
                    {
                        return new ChallengeResult();
                    }

                    _productRepository.UpdateProduct(product);

                    _transactionRepository.AddTransaction(new Transaction()
                    {
                        ProductId = product.Id,
                        ProductCode = product.ProductCode,
                        Name = product.Name,
                        Size = product.Size,
                        DateOfTransaction = useViewModel.DateUsed,
                        TypeOfTransaction = "Use",
                        Quantity = useViewModel.Quantity,
                        Description = useViewModel.Description,
                        OwnerID = _userManager.GetUserId(User)
                    });

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "Quantity used must be less than or equal to stock available";
                }
            }
            return View(useViewModel);
        }

        public IActionResult Edit(int id)
        {
            var product = _productRepository.GetProductByProductCode(
                id, _userManager.GetUserId(User));
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.UpdateProduct(product);
                return RedirectToAction("Index", "Home");
            }
            return View(product);
        }
    }
}
