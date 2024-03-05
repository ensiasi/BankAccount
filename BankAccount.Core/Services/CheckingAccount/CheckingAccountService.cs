using BankAccount.Core.Exceptions;
using BankAccount.Core.Models;
using BankAccount.Core.Ports.Driven;
using BankAccount.Core.Ports.Driving;

namespace BankAccount.Core.Services.CheckingAccounts
{
    public class CheckingAccountService : IAccountService<CheckingAccount>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IOperationHistoryService _operationHistoryService;
        private readonly IOverDraftEligibilityService _overDraftEligibilityService;
        public CheckingAccountService(IAccountRepository accountRepository,
                              IOverDraftEligibilityService overDraftEligibilityService,
                              IOperationHistoryService operationHistoryService)
        {
            _accountRepository = accountRepository;
            _overDraftEligibilityService = overDraftEligibilityService;
            _operationHistoryService = operationHistoryService;
        }
        public async Task<CheckingAccount> Deposit(string accountNumber, decimal amount)
        {
            var account = await _accountRepository.GetCheckingAccount(accountNumber);
            account.CheckingBalance += amount;
            await _accountRepository.UpdateCheckingAccountBalance(account);
            await _operationHistoryService.AddOperation(new Operation { AccountNumber= account.AccountNumber,AccountType= AccountType.Checking,Amount=amount,Date=DateTime.Now,OperationType=OperationType.Deposit});
            return account;
        }
        public async Task<CheckingAccount> Withdraw(string accountNumber, decimal amount)
        {
            var account = await _accountRepository.GetCheckingAccount(accountNumber);
            var balanceAfterWithdraw = account.CheckingBalance - amount;

            if (balanceAfterWithdraw < 0)
            {
                if (!_overDraftEligibilityService.IsOverDraftEligible(account))
                {
                    throw new InsufficientFundsException("Insufficient balance for withdrawal");
                }
                else if (_overDraftEligibilityService.IsDraftLimitExceeded(account, balanceAfterWithdraw))
                {
                    throw new OverDraftLimitExceededException("Overdraft limit exceeded");
                }
            }
            account.CheckingBalance = balanceAfterWithdraw;
            await _accountRepository.UpdateCheckingAccountBalance(account);
            await _operationHistoryService.AddOperation(new Operation { AccountNumber=account.AccountNumber,AccountType=AccountType.Checking,Amount= -amount,Date=DateTime.Now,OperationType=OperationType.Withdrawal});
            return account;
        }

    }
}
