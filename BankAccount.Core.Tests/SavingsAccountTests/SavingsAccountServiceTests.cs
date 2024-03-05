using BankAccount.Core.Exceptions;
using BankAccount.Core.Models;
using BankAccount.Core.Ports.Driven;
using BankAccount.Core.Ports.Driving;
using BankAccount.Core.Services.SavingsAccounts;
using Moq;
using Xunit;

namespace BankAccount.Core.Tests.SavingsAccountTests
{
    public class SavingsAccountServiceTests
    {
        private readonly Mock<IAccountRepository<SavingsAccount>> _accountRepository;
        private readonly SavingsAccountService _savingsAccountService;
        private readonly Mock<IOperationHistoryService> _operationHistoryService;
        private SavingsAccount _account;
        public SavingsAccountServiceTests()
        {
            _accountRepository = new Mock<IAccountRepository<SavingsAccount>>();
            _operationHistoryService = new Mock<IOperationHistoryService>();
            _savingsAccountService = new SavingsAccountService(_accountRepository.Object,_operationHistoryService.Object);
            _account = new SavingsAccount
            {
                AccountId =123,
                AccountNumber = "123",
                Balance = 100,
                DepositCeiling = 1000
            };
            _accountRepository.Setup(x => x.GetById(123))
                .ReturnsAsync(_account);
            _accountRepository.Setup(x => x.Save(It.IsAny<SavingsAccount>()))
                .Returns(Task.CompletedTask);
            _operationHistoryService.Setup(x => x.AddOperation(It.IsAny<Operation>()))
                .ReturnsAsync(new Operation());
        }
        [Fact]
        public async void Deposit_ValidAmount_ShouldUpdateBalance()
        {
            //Act
            var result = await _savingsAccountService.Deposit(_account, 100);
            //Assert
            Assert.Equal(200, result.Balance);
        }
        [Fact]
        public void Deposit_InvalidAmount_ShouldThrowException()
        {
            //Act
            var exception = Assert.ThrowsAsync<DepositCeilingReachedException>(() => _savingsAccountService.Deposit(_account, 1000));
            //Assert
            Assert.Equal("Deposit amount exceeds the limit.", exception.Result.Message);
        }
        [Fact]
        public async void Withdraw_ValidAmount_ShouldDecreaseBalance()
        {
            //Act
            var result = await _savingsAccountService.Withdraw(_account, 50);
            //Assert
            Assert.Equal(50, result.Balance);
        }
        [Fact]
        public void Withdraw_InvalidAmount_ShouldThrowException()
        {
            //Act
            var exception = Assert.ThrowsAsync<InsufficientFundsException>(() => _savingsAccountService.Withdraw(_account, 200));
            //Assert
            Assert.Equal("Withdrawal amount exceeds the balance.", exception.Result.Message);
        }
    }
}
