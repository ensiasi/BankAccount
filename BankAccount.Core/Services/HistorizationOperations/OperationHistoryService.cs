using BankAccount.Core.Models;
using BankAccount.Core.Ports.Driven;
using BankAccount.Core.Ports.Driving;

namespace BankAccount.Core.Services.HistorizationOperations
{
    public class OperationHistoryService : IOperationHistoryService
    {
        private readonly IOperationHistoryRepository _operationHistoryRepository;
        public OperationHistoryService(IOperationHistoryRepository operationHistoryRepository)
        {
                _operationHistoryRepository = operationHistoryRepository;
        }
        public async Task<Operation> AddOperation(Operation operation)
        {
            await _operationHistoryRepository.AddOperation(operation);
            return operation;
        }
        public async Task<IEnumerable<Operation>> GetOperations(int accountId, DateTime startDate, DateTime endDate)
        {
            var operations = await _operationHistoryRepository.GetOperations(accountId,startDate,endDate);
            return operations;
        }
    }
}
