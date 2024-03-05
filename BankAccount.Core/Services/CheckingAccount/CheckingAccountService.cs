using BankAccount.Core.Models;
using BankAccount.Core.Ports.Driven;
using BankAccount.Core.Ports.Driving;

namespace BankAccount.Core.Services.CheckingAccounts
{
    public class CheckingAccountService : IAccountService<CheckingAccount>
    {
        private readonly IAccountRepository<CheckingAccount> _accountRepository;
        private readonly IOperationHistoryService _operationHistoryService;
        private readonly IOverDraftEligibilityService _overDraftEligibilityService;
        public CheckingAccountService(IAccountRepository<CheckingAccount> accountRepository,
                              IOverDraftEligibilityService overDraftEligibilityService,
                              IOperationHistoryService operationHistoryService)
        {
            _accountRepository = accountRepository;
            _overDraftEligibilityService = overDraftEligibilityService;
            _operationHistoryService = operationHistoryService;
        }
        public async Task<CheckingAccount> Deposit(CheckingAccount account, decimal amount)
        {
            account.Balance += amount;
            await _accountRepository.Save(account);
            //record transaction
            await _operationHistoryService.AddOperation(new Operation
            {
                AccountId = account.AccountId,
                Amount = amount,
                OperationDate = DateTime.Now,
                OperationType = OperationType.Deposit
            });
            return account;
        }
        public async Task<CheckingAccount> Withdraw(CheckingAccount account, decimal amount)
        {
            _overDraftEligibilityService.CheckOverDraft(account, amount);
            account.Balance -= amount;
            await _accountRepository.Save(account);
            await _operationHistoryService.AddOperation(new Operation
            {
                AccountId = account.AccountId,
                Amount = -amount,
                OperationDate = DateTime.Now,
                OperationType = OperationType.Withdrawal
            });
            return account;
        }


    }
}
