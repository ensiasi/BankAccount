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
        public async Task<List<Operation>> GetOperations(string accountNumber)
        {
            var operations = await _operationHistoryRepository.GetOperations(accountNumber);
            return operations;
        }
    }
}
