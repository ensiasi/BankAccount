using BankAccount.Core.Models;
using BankAccount.Core.Ports.Driven;
using BankAccount.Core.Ports.Driving;
using BankAccount.Core.Services.CheckingAccounts;
using Moq;
using Xunit;

namespace BankAccount.Core.Tests.CheckingAccountTests
{
    public class CheckingAccountStatementServiceTest
    {
        private readonly CheckingAccountStatementService _checkingAccountStatementService;
        private readonly Mock<IAccountRepository> _accountRepository;
        private readonly Mock<IOperationHistoryService> _operationHistoryService;
        private readonly CheckingAccount _account;
        private readonly List<Operation> _operations;
        private readonly AccountStatement _accountStatement;
        public CheckingAccountStatementServiceTest()
        {
            _account = new CheckingAccount
            {
                AccountNumber = "1234567890",
                IsOverDraftEnabled = true,
                OverDraftLimit = 1000,
                Balance = 50
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
            _accountRepository = new Mock<IAccountRepository>();
            _accountRepository.Setup(x => x.GetCheckingAccount(It.IsAny<string>())).ReturnsAsync(_account);
            _operationHistoryService = new Mock<IOperationHistoryService>();
            _operationHistoryService.Setup(x => x.GetOperations(It.IsAny<int>(),It.IsAny<DateTime>(),It.IsAny<DateTime>())).ReturnsAsync(_operations);
            _checkingAccountStatementService = new CheckingAccountStatementService(_accountRepository.Object, _operationHistoryService.Object);
        }
        [Fact]
        public async void GetAccountStatement_ShouldReturnAccountStatement()
        {
            // Act
            var result = await _checkingAccountStatementService.GetAccountStatement(_account,DateTime.Now, DateTime.Now);
            // Assert
            Assert.Equal(_accountStatement.Balance, result.Balance);
            Assert.Equal(_accountStatement.Operations.ToList().Count, result.Operations.ToList().Count);
        }
    }
}
