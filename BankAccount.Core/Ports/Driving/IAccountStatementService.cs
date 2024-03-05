using BankAccount.Core.Models;
namespace BankAccount.Core.Ports.Driving
{
    public interface IAccountStatementService
    {
        Task<AccountStatement> GetAccountStatement(string accountNumber);
    }
}
