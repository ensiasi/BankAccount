
namespace BankAccount.Core.Models
{
    public class AccountStatement
    {
        public decimal Balance { get; set; }
        public List<Operation> Operations { get; set; }
    }
}
