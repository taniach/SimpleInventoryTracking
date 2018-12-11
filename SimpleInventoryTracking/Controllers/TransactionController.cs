using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleInventoryTracking.Models;
using SimpleInventoryTracking.ViewModels;
using System.Linq;

namespace SimpleInventoryTracking.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;
        private IAuthorizationService _authorizationService { get; }
        private UserManager<IdentityUser> _userManager { get; }

        public TransactionController(
            ITransactionRepository transactionRepository,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
        {
            _transactionRepository = transactionRepository;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var currentUserId = _userManager.GetUserId(User);

            var transactions = _transactionRepository.GetAllTransactions(currentUserId)
                .OrderByDescending(t => t.DateOfTransaction);

            var transactionViewModel = new TransactionViewModel()
            {
                Transactions = transactions.ToList()
            };

            return View(transactionViewModel);
        }

        public IActionResult TransactionsByProductCode(int id)
        {
            var transactions = _transactionRepository.GetTransactionsByProductCode(
                id, _userManager.GetUserId(User))
                .OrderByDescending(t => t.DateOfTransaction);

            var transactionViewModel = new TransactionViewModel()
            {
                Transactions = transactions.ToList()
            };

            return View(transactionViewModel);
        }

        public IActionResult Delete(int id)
        {
            var transaction = _transactionRepository.GetTransactionsByProductCode(
                id, _userManager.GetUserId(User));

            _transactionRepository.DeleteTransaction(id);
            return RedirectToAction("Index", "Transaction");
        }
    }
}
