
using AppControleFinanceiro.Backend.Enums;

namespace AppControleFinanceiro.Backend.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TransactionType Type { get; set; }
        public int DateTimeOffSet { get; set; }
        public decimal Value { get; set; }
    }
}
