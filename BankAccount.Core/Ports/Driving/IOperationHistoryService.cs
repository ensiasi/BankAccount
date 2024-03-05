using BankAccount.Core.Models;

namespace BankAccount.Core.Ports.Driving
{
    public interface IOperationHistoryService
    {
        Task<List<Operation>> GetOperations(string accountNumber);
        Task<Operation> AddOperation(Operation operation);
    }
}
