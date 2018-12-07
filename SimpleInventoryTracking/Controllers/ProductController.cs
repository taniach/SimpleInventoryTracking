using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleInventoryTracking.Models;
using SimpleInventoryTracking.ViewModels;

namespace SimpleInventoryTracking.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ITransactionRepository _transactionRepository;

        public ProductController(
            IProductRepository productRepository,
            ITransactionRepository transactionRepository)
        {
            _productRepository = productRepository;
            _transactionRepository = transactionRepository;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.AddProduct(product);
                return RedirectToAction("Index", "Home");
            }
            return View(product);
        }

        public IActionResult AddProductComplete()
        {
            return View();
        }

        public IActionResult Delete(string productCode)
        {
            _productRepository.DeleteProduct(productCode);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Purchase(string productCode)
        {
            PurchaseViewModel purchaseViewModel = new PurchaseViewModel();
            purchaseViewModel.ProductCode = productCode;
            return View(purchaseViewModel);
        }

        [HttpPost]
        public IActionResult Purchase(PurchaseViewModel purchaseViewModel)
        {
            if (ModelState.IsValid)
            {
                var product = _productRepository.GetProductByProductCode(
                    purchaseViewModel.ProductCode);

                product.Purchase(purchaseViewModel.Quantity);
                _productRepository.UpdateProduct(product);

                _transactionRepository.AddTransaction(new Transaction() {
                    ProductCode = product.ProductCode,
                    Name = product.Name,
                    Size = product.Size,
                    DateOfTransaction = purchaseViewModel.DatePurchased,
                    TypeOfTransaction = "Purchase",
                    Quantity = purchaseViewModel.Quantity,
                    Description = purchaseViewModel.Description
                });

                return RedirectToAction("Index", "Home");
            }
            return View(purchaseViewModel);
        }

        public IActionResult Use(string productCode)
        {
            UseViewModel useViewModel = new UseViewModel();
            useViewModel.ProductCode = productCode;
            return View(useViewModel);
        }

        [HttpPost]
        public IActionResult Use(UseViewModel useViewModel)
        {
            if (ModelState.IsValid)
            {
                var product = _productRepository.GetProductByProductCode(
                    useViewModel.ProductCode);

                if (product.Use(useViewModel.Quantity))
                {
                    _productRepository.UpdateProduct(product);

                    _transactionRepository.AddTransaction(new Transaction()
                    {
                        ProductCode = product.ProductCode,
                        Name = product.Name,
                        Size = product.Size,
                        DateOfTransaction = useViewModel.DateUsed,
                        TypeOfTransaction = "Use",
                        Quantity = useViewModel.Quantity,
                        Description = useViewModel.Description
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

        public IActionResult Edit(string productCode)
        {
            var product = _productRepository.GetProductByProductCode(productCode);
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
