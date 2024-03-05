namespace BankAccount.Core.Models
{
    public class Operation
    {
        public string? AccountNumber { get; set; }
        public AccountType AccountType { get; set; }
        public OperationType OperationType { get; set; }

        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}
