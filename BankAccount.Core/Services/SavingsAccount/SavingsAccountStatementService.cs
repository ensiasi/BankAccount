using BankAccount.Core.Models;
using BankAccount.Core.Ports.Driven;
using BankAccount.Core.Ports.Driving;

namespace BankAccount.Core.Services.SavingsAccounts
{
    public class SavingsAccountStatementService : IAccountStatementService<SavingsAccount>
    {
        private IAccountRepository _accountRepository;
        private IOperationHistoryService _operationHistoryService;

        public SavingsAccountStatementService(IAccountRepository accountRepository, IOperationHistoryService operationHistoryService)
        {
            _accountRepository = accountRepository;
            _operationHistoryService = operationHistoryService;
        }

        public async Task<AccountStatement> GetAccountStatement(SavingsAccount Svaccount, DateTime startDate, DateTime endDate)
        {
            var account = await _accountRepository.GetSavingsAccount(Svaccount.AccountNumber);
            var operations = await _operationHistoryService.GetOperations(Svaccount.AccountId,startDate,endDate);
            var balance = account.Balance;
            return new AccountStatement
            {
                Balance = balance,
                Operations = operations
            };
        }
    }
}
