// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using BankAccount.Core.Exceptions;
using BankAccount.Core.Models;
using BankAccount.Core.Ports.Driven;
using BankAccount.Core.Ports.Driving;
using System.Runtime.Serialization;

namespace BankAccount.Core.Services.SavingsAccounts
{
    public class SavingsAccountService : IAccountService<SavingsAccount>
    {
        private IAccountRepository _accountRepository;
        private IOperationHistoryService _operationHistoryService;

        public SavingsAccountService(IAccountRepository accountRepository, IOperationHistoryService operationHistoryService)
        {
            _accountRepository = accountRepository;
            _operationHistoryService = operationHistoryService;
        }

        public async Task<SavingsAccount> Deposit(string accountNumber, decimal amount)
        {
            var account = await _accountRepository.GetSavingsAccount(accountNumber);
            var newBalance = account.Balance + amount;
            if (newBalance > account.DepositCeiling)
            {
                throw new DepositCeilingReachedException("Deposit ceiling reached");
            }
            account.Balance = newBalance;
            await _accountRepository.UpdateSavingsAccount(account);
            await _operationHistoryService.AddOperation(new Operation { AccountId= account.AccountId,AccountType=AccountType.Savings,Amount=amount,OperationType=OperationType.Deposit, OperationDate = DateTime.Now});
            return account;
        }
        public async Task<SavingsAccount> Withdraw(string accountNumber, decimal amount)
        {
            var account = await _accountRepository.GetSavingsAccount(accountNumber);
            var newBalance = account.Balance - amount;
            if (newBalance < 0)
            {
                throw new InsufficientFundsException("Insufficient balance for withdrawal");
            }
            account.Balance = newBalance;
            await _accountRepository.UpdateSavingsAccount(account);
            await _operationHistoryService.AddOperation(new Operation { AccountId = account.AccountId, AccountType = AccountType.Savings, Amount = -amount, OperationType = OperationType.Withdrawal, OperationDate = DateTime.Now });
            return account;
        }
    }
}
