namespace BankAccount.Core.Models
{
    public class CheckingAccount : Account
    {
        public bool IsOverDraftEnabled { get; set; }
        public decimal OverDraftLimit { get; set; }
    }
}
