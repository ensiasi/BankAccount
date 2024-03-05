using BankAccount.Core.Models;
using BankAccount.Core.Ports.Driven;
using BankAccount.Core.Ports.Driving;
using BankAccount.Core.Services.SavingsAccounts;
using Moq;
using Xunit;

namespace BankAccount.Core.Tests.SavingsAccountTests
{
    public class SavingsAccountStatementServiceTest
    {
        private readonly SavingsAccountStatementService _savingsAccountStatementService;
        private readonly Mock<IAccountRepository> _accountRepository;
        private readonly Mock<IOperationHistoryService> _operationHistoryService;
        private readonly SavingsAccount _account;
        private readonly List<Operation> _operations;
        private readonly AccountStatement _accountStatement;
        public SavingsAccountStatementServiceTest()
        {
            _account = new SavingsAccount
            {
                AccountNumber = "1234567890",
                SavingsBalance = 50,
                DepositCeiling = 100
            };
            _operations = new List<Operation>
            {
                new Operation
                {
                    Amount = 100,
                    OperationType = OperationType.Deposit,
                    Date = DateTime.Now
                },
                new Operation
                {
                    Amount = 50,
                    OperationType = OperationType.Withdrawal,
                    Date = DateTime.Now
                }
            };
            _accountStatement = new AccountStatement
            {
                Balance = 50,
                Operations = _operations
            };
            _accountRepository = new Mock<IAccountRepository>();
            _accountRepository.Setup(x => x.GetSavingsAccount(It.IsAny<string>())).ReturnsAsync(_account);
            _operationHistoryService = new Mock<IOperationHistoryService>();
            _operationHistoryService.Setup(x => x.GetOperations(It.IsAny<string>())).ReturnsAsync(_operations);
            _savingsAccountStatementService = new SavingsAccountStatementService(_accountRepository.Object, _operationHistoryService.Object);
        }
        [Fact]
        public async void GetAccountStatement_ShouldReturnAccountStatement()
        {
            // Act
            var result = await _savingsAccountStatementService.GetAccountStatement("1234567890");
            // Assert
            Assert.Equal(_accountStatement.Balance, result.Balance);
            Assert.Equal(_accountStatement.Operations.Count, result.Operations.Count);
        }
    }
}
