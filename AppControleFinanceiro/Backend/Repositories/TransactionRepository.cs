using AppControleFinanceiro.Backend.Models;
using AppControleFinanceiro.Backend.Repositories.Interfaces;
using LiteDB;

namespace AppControleFinanceiro.Backend.Repositories
{
    internal class TransactionRepository : ITransactionRepository
    {
        private readonly LiteDatabase _database;
        private readonly string _collectionName = "transactions";
        public TransactionRepository()
        {
            var _database = new LiteDatabase("Filename=c:/temp/AppControleFinanceiro.db;Connection=Shared");
        }

        public List<Transaction> GetAll()
        {
            return _database.GetCollection<Transaction>(_collectionName).Query().OrderByDescending(x => x.Date).ToList();
        }
        public Transaction GetById(int id)
        {
            return _database.GetCollection<Transaction>(_collectionName).Query().Where(x => x.Id.Equals(id)).SingleOrDefault();
        }

        public void Add(Transaction transaction)
        {
            var collection = _database.GetCollection<Transaction>(_collectionName);
            collection.Insert(transaction);
            collection.EnsureIndex(i => i.Date);
        }
        public void Update(Transaction transaction)
        {
            var collection = _database.GetCollection<Transaction>(_collectionName);
            collection.Update(transaction);
        }
        public void Delete(Transaction transaction)
        {
            var collection = _database.GetCollection<Transaction>(_collectionName);
            collection.Delete(transaction.Id);
        }


    }
}
