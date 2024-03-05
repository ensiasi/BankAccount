using BankAccount.Core.Models;
namespace BankAccount.Core.Ports.Driving
{
    public interface IAccountStatementService<T> where T : Account
    {
        Task<AccountStatement> GetAccountStatement(T account, DateTime startdate,DateTime endDate);
    }
}
