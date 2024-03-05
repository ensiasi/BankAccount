// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this
using BankAccount.Core.Exceptions;
using BankAccount.Core.Models;
using BankAccount.Core.Ports.Driven;
using BankAccount.Core.Ports.Driving;
using BankAccount.Core.Services.CheckingAccounts;
using Moq;
using Xunit;
namespace BankAccount.Core.Tests.CheckingAccountTests
{
    public class CheckingAccountServiceTests
    {
        private const string AccountNumber = "123456";
        private const decimal InitialBalance = 100;
        private readonly Mock<IAccountRepository<CheckingAccount>> _accountRepository;
        private readonly Mock<IOverDraftEligibilityService> _overDraftEligibilityService;
        private readonly Mock<IOperationHistoryService> _operationHistoryService;
        private readonly CheckingAccountService _accountService;
        private readonly CheckingAccount _account;

        public CheckingAccountServiceTests()
        {
            _accountRepository = new Mock<IAccountRepository<CheckingAccount>>();
            _overDraftEligibilityService = new Mock<IOverDraftEligibilityService>();
            _operationHistoryService = new Mock<IOperationHistoryService>();
            _accountService = new CheckingAccountService(_accountRepository.Object, _overDraftEligibilityService.Object, _operationHistoryService.Object);
            _account = new CheckingAccount
            {
                AccountId = 1,
                AccountNumber = AccountNumber,
                Balance = InitialBalance,
                IsOverDraftEnabled = true,
                OverDraftLimit = 150
            };
            _accountRepository.Setup(x => x.GetById(1)).ReturnsAsync(_account);
            _operationHistoryService.Setup(x => x.AddOperation(It.IsAny<Operation>())).ReturnsAsync(new Operation());
        }

        [Fact]
        public void Deposit_WithValidAmount_IncreasesBalance()
        {
            _accountRepository.Setup(x => x.Save(_account)).Returns(Task.CompletedTask);
            var result = _accountService.Deposit(_account, 100).Result;
            Assert.Equal(200, result.Balance);
        }

        [Fact]
        public void Withdraw_WithSufficientBalance_DecreasesBalance()
        {
            _accountRepository.Setup(x => x.Save(_account)).Returns(Task.CompletedTask);
            var result = _accountService.Withdraw(_account, 50).Result;
            Assert.Equal(50, result.Balance);
        }

        [Fact]
        public void Withdraw_WithInsufficientBalance_ThrowsInsufficientFundsException()
        {
            _overDraftEligibilityService.Setup(x => x.CheckOverDraft(_account, It.IsAny<decimal>())).Throws(new InsufficientFundsException("Insufficient balance for withdrawal"));
            var exception = Assert.ThrowsAsync<InsufficientFundsException>(() => _accountService.Withdraw(_account, 200));
            Assert.Equal("Insufficient balance for withdrawal", exception.Result.Message);
        }
        [Fact]
        public void Withdraw_WithInsufficientBalance_ThrowsOverDraftLimitExceededException()
        {
            _overDraftEligibilityService.Setup(x => x.CheckOverDraft(_account, It.IsAny<decimal>())).Throws(new OverDraftLimitExceededException("Overdraft limit exceeded"));
            var exception = Assert.ThrowsAsync<OverDraftLimitExceededException>(() => _accountService.Withdraw(_account, 200));
            Assert.Equal("Overdraft limit exceeded", exception.Result.Message);
        }
    }
}
