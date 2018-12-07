using System.Collections.Generic;
using System.Linq;

namespace SimpleInventoryTracking.Models
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _appDbContext;

        public TransactionRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddTransaction(Transaction transaction)
        {
            _appDbContext.Transactions.Add(transaction);
            _appDbContext.SaveChanges();
        }

        public void DeleteTransaction(int id)
        {
            _appDbContext.Transactions.Remove(_appDbContext.Transactions.Find(id));
            _appDbContext.SaveChanges();
        }

        public Transaction FindTransaction(int id)
        {
            return _appDbContext.Transactions.FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            return _appDbContext.Transactions;
        }

        public IEnumerable<Transaction> GetTransactionsByProductCode(string productCode)
        {
            return _appDbContext.Transactions.Where(t => t.ProductCode.Equals(productCode));
        }

        public void UpdateTransaction(Transaction transaction)
        {
            _appDbContext.Transactions.Update(transaction);
            _appDbContext.SaveChanges();
        }
    }
}
