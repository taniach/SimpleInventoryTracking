using System.Collections.Generic;

namespace SimpleInventoryTracking.Models
{
    public interface ITransactionRepository
    {
        IEnumerable<Transaction> GetAllTransactions(string ownerId);

        void AddTransaction(Transaction transaction);

        void UpdateTransaction(Transaction transaction);

        void DeleteTransaction(int id);

        IEnumerable<Transaction> GetTransactionsByProductCode(
            int id, string ownerId);
    }
}
