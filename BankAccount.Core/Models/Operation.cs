namespace BankAccount.Core.Models
{
    public class Operation
    {
        public int AccountId { get; set; }
        public AccountType AccountType { get; set; }
        public OperationType OperationType { get; set; }

        public DateTime OperationDate { get; set; }
        public decimal Amount { get; set; }
    }
}
