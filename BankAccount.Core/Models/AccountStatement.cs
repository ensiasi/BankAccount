
namespace BankAccount.Core.Models
{
    public class AccountStatement
    {
        public decimal Balance { get; set; }
        public IEnumerable<Operation> Operations { get; set; }
    }
}
