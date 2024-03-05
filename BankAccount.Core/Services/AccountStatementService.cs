using BankAccount.Core.Models;
using BankAccount.Core.Ports.Driven;
using BankAccount.Core.Ports.Driving;

namespace BankAccount.Core.Services
{
    public class AccountStatementService : IAccountStatementService
    {
        private IOperationHistoryService _operationHistoryService;
        public AccountStatementService(IOperationHistoryService operationHistoryService)
        {
            _operationHistoryService = operationHistoryService;
        }

        public async Task<AccountStatement> GetAccountStatement(Account account, DateTime startDate, DateTime endDate)
        {
            var operations = await _operationHistoryService.GetOperations(account.AccountId, startDate, endDate);
            var balance = account.Balance;
            return new AccountStatement
            {
                Balance = balance,
                Operations = operations
            };
        }
    }
}
