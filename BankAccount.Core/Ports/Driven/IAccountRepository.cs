using BankAccount.Core.Models;

namespace BankAccount.Core.Ports.Driven
{
    public interface IAccountRepository<T> where T : Account
    {
        Task Save(T account);
        Task<T> GetById(int id);
    }
}
