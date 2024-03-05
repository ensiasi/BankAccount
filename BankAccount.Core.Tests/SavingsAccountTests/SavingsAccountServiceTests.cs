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
        private readonly Mock<IAccountRepository> _accountRepository;
        private readonly SavingsAccountService _savingsAccountService;
        private readonly Mock<IOperationHistoryService> _operationHistoryService;
        private SavingsAccount _account;
        public SavingsAccountServiceTests()
        {
            _accountRepository = new Mock<IAccountRepository>();
            _operationHistoryService = new Mock<IOperationHistoryService>();
            _savingsAccountService = new SavingsAccountService(_accountRepository.Object,_operationHistoryService.Object);
            _account = new SavingsAccount
            {
                AccountNumber = "123",
                Balance = 100,
                DepositCeiling = 1000
            };
            _accountRepository.Setup(x => x.GetSavingsAccount("123")).ReturnsAsync(_account);
            _accountRepository.Setup(x => x.UpdateSavingsAccount(It.IsAny<SavingsAccount>())).ReturnsAsync(_account);
            _operationHistoryService.Setup(x => x.AddOperation(It.IsAny<Operation>())).ReturnsAsync(new Operation());
        }
        [Fact]
        public async void Deposit_ValidAmount_ShouldUpdateBalance()
        {
            //Act
            var result = await _savingsAccountService.Deposit("123", 100);
            //Assert
            Assert.Equal(200, result.Balance);
        }
        [Fact]
        public void Deposit_InvalidAmount_ShouldThrowException()
        {
            //Act
            var exception = Assert.ThrowsAsync<DepositCeilingReachedException>(() => _savingsAccountService.Deposit("123", 1000));
            //Assert
            Assert.Equal("Deposit ceiling reached", exception.Result.Message);
        }
        [Fact]
        public async void Withdraw_ValidAmount_ShouldDecreaseBalance()
        {
            //Act
            var result = await _savingsAccountService.Withdraw("123", 50);
            //Assert
            Assert.Equal(50, result.Balance);
        }
        [Fact]
        public void Withdraw_InvalidAmount_ShouldThrowException()
        {
            //Act
            var exception = Assert.ThrowsAsync<InsufficientFundsException>(() => _savingsAccountService.Withdraw("123", 200));
            //Assert
            Assert.Equal("Insufficient balance for withdrawal", exception.Result.Message);
        }
    }
}
