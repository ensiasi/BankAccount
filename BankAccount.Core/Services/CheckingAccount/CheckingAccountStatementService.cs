using BankAccount.Core.Models;
using BankAccount.Core.Ports.Driven;
using BankAccount.Core.Ports.Driving;

namespace BankAccount.Core.Services.CheckingAccounts
{
    public class CheckingAccountStatementService : IAccountStatementService<CheckingAccount>
    {
        private IAccountRepository _accountRepository;
        private IOperationHistoryService _operationHistoryService;

        public CheckingAccountStatementService(IAccountRepository accountRepository, IOperationHistoryService operationHistoryService)
        {
            _accountRepository = accountRepository;
            _operationHistoryService = operationHistoryService;
        }

        public async Task<AccountStatement> GetAccountStatement(CheckingAccount chAccount, DateTime startDate, DateTime endDate)
        {
            var account = await _accountRepository.GetCheckingAccount(chAccount.AccountNumber);
            var operations = await _operationHistoryService.GetOperations(chAccount.AccountId,startDate,endDate);
            var balance = account.Balance;
            return new AccountStatement
            {
                Balance = balance,
                Operations = operations
            };
        }
    }
}
