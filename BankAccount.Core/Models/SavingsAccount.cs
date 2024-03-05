namespace BankAccount.Core.Models
{
    public class SavingsAccount : Account
    {
        public decimal DepositCeiling { get; set; }
        public decimal SavingsBalance { get; set; }
    }
}
