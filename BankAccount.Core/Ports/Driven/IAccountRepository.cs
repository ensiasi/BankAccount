using BankAccount.Core.Models;

namespace BankAccount.Core.Ports.Driven
{
    public interface IAccountRepository
    {
        Task<CheckingAccount> GetCheckingAccount(string accountNumber);
        Task<SavingsAccount> GetSavingsAccount(string accountNumber);
        Task<CheckingAccount> UpdateCheckingAccountBalance(CheckingAccount account);
        Task<SavingsAccount> UpdateSavingsAccount(SavingsAccount account);
    }
}
