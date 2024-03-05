using BankAccount.Core.Models;

namespace BankAccount.Core.Ports.Driven
{
    public interface IOperationHistoryRepository
    {
        Task<List<Operation>> GetOperations(string accountNumber);
        Task<Operation> AddOperation(Operation operation);

    }
}
