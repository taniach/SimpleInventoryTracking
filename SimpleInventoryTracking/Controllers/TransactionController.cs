using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleInventoryTracking.Models;
using SimpleInventoryTracking.ViewModels;
using System.Linq;

namespace SimpleInventoryTracking.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var transactions = _transactionRepository.GetAllTransactions()
                .OrderByDescending(t => t.DateOfTransaction);

            var transactionViewModel = new TransactionViewModel()
            {
                Transactions = transactions.ToList()
            };

            return View(transactionViewModel);
        }

        public IActionResult TransactionsByProductCode(string productCode)
        {
            var transactions = _transactionRepository.GetTransactionsByProductCode(productCode)
                .OrderByDescending(t => t.DateOfTransaction);

            var transactionViewModel = new TransactionViewModel()
            {
                Transactions = transactions.ToList()
            };

            return View(transactionViewModel);
        }
    }
}
