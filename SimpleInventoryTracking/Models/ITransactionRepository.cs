using System.Collections.Generic;

namespace SimpleInventoryTracking.Models
{
    public interface ITransactionRepository
    {
        IEnumerable<Transaction> GetAllTransactions();

        Transaction FindTransaction(int id);

        void AddTransaction(Transaction transaction);

        void UpdateTransaction(Transaction transaction);

        void DeleteTransaction(int id);

        IEnumerable<Transaction> GetTransactionsByProductCode(string productCode);
    }
}
