using BankAccount.Core.Models;

namespace BankAccount.Core.Ports.Driving
{
    public interface IOperationHistoryService
    {
        Task<IEnumerable<Operation>> GetOperations(int accountId, DateTime startDate, DateTime endDate);
        Task<Operation> AddOperation(Operation operation);
    }
}
