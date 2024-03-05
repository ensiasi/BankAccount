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
        private IAccountRepository<SavingsAccount> _accountRepository;
        private IOperationHistoryService _operationHistoryService;

        public SavingsAccountService(IAccountRepository<SavingsAccount> accountRepository, IOperationHistoryService operationHistoryService)
        {
            _accountRepository = accountRepository;
            _operationHistoryService = operationHistoryService;
        }

        public async Task<SavingsAccount> Deposit(SavingsAccount account, decimal amount)
        {
            if (account.Balance + amount > account.DepositCeiling)
            {
                throw new DepositCeilingReachedException("Deposit amount exceeds the limit.");
            }
            account.Balance += amount;
            await _accountRepository.Save(account);
            await _operationHistoryService.AddOperation(new Operation
            {
                AccountId = account.AccountId,
                Amount = amount,
                OperationDate = DateTime.Now,
                OperationType = OperationType.Deposit
            });
            return account;
        }

        public async Task<SavingsAccount> Withdraw(SavingsAccount account, decimal amount)
        {
            if (amount > account.Balance)
            {
                throw new InsufficientFundsException("Withdrawal amount exceeds the balance.");
            }
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
