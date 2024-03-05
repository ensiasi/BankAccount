using BankAccount.Core.Models;

namespace BankAccount.Core.Ports.Driven
{
    public interface IOperationHistoryRepository
    {
        Task<IEnumerable<Operation>> GetOperations(int accountId, DateTime startDate, DateTime endDate);
        Task<Operation> AddOperation(Operation operation);

    }
}
