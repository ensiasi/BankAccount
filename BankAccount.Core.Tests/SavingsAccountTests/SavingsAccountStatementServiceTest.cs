using BankAccount.Core.Models;
using BankAccount.Core.Ports.Driving;
using BankAccount.Core.Services;
using Moq;
using Xunit;

namespace BankAccount.Core.Tests.SavingsAccountTests
{
    public class SavingsAccountStatementServiceTest
    {
        private readonly IAccountStatementService _accountStatementService;
        private readonly Mock<IOperationHistoryService> _operationHistoryService;
        private readonly SavingsAccount _account;
        private readonly List<Operation> _operations;
        private readonly AccountStatement _accountStatement;
        public SavingsAccountStatementServiceTest()
        {
            _account = new SavingsAccount
            {
                AccountNumber = "1234567890",
                Balance = 50,
                DepositCeiling = 100
            };
            _operations = new List<Operation>
            {
                new Operation
                {
                    Amount = 100,
                    OperationType = OperationType.Deposit,
                    OperationDate = DateTime.Now
                },
                new Operation
                {
                    Amount = 50,
                    OperationType = OperationType.Withdrawal,
                    OperationDate = DateTime.Now
                }
            };
            _accountStatement = new AccountStatement
            {
                Balance = 50,
                Operations = _operations
            };
            _operationHistoryService = new Mock<IOperationHistoryService>();
            _operationHistoryService.Setup(x => x.GetOperations(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(_operations);
            _accountStatementService = new AccountStatementService(_operationHistoryService.Object);
        }
        [Fact]
        public async void GetAccountStatement_ShouldReturnAccountStatement()
        {
            // Act
            var result = await _accountStatementService.GetAccountStatement(_account,DateTime.Now,DateTime.Now);
            // Assert
            Assert.Equal(_accountStatement.Balance, result.Balance);
            Assert.Equal(_accountStatement.Operations.ToList().Count, result.Operations.ToList().Count);
        }
    }
}
