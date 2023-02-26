using AppControleFinanceiro.Backend.Models;

namespace AppControleFinanceiro.Backend.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        List<Transaction> GetAll();
        Transaction GetById(int id);
        void Add(Transaction transaction);
        void Update(Transaction transaction);
        void Delete(Transaction transaction);
    }
}