using BankAccount.Core.Models;
using BankAccount.Core.Ports.Driven;
using BankAccount.Core.Ports.Driving;

namespace BankAccount.Core.Services.SavingsAccounts
{
    public class SavingsAccountStatementService : IAccountStatementService
    {
        private IAccountRepository _accountRepository;
        private IOperationHistoryService _operationHistoryService;

        public SavingsAccountStatementService(IAccountRepository accountRepository, IOperationHistoryService operationHistoryService)
        {
            _accountRepository = accountRepository;
            _operationHistoryService = operationHistoryService;
        }

        public async Task<AccountStatement> GetAccountStatement(string accountNumber)
        {
            var account = await _accountRepository.GetSavingsAccount(accountNumber);
            var operations = await _operationHistoryService.GetOperations(accountNumber);
            var balance = account.SavingsBalance;
            return new AccountStatement
            {
                Balance = balance,
                Operations = operations
            };
        }
    }
}
