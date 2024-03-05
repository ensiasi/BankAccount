using BankAccount.Core.Models;
using BankAccount.Core.Ports.Driven;
using BankAccount.Core.Services.HistorizationOperations;
using Moq;
using Xunit;
namespace BankAccount.Core.Tests.OperationsHistory
{
    public class OperationHistoryServiceTests
    {
        private readonly Mock<IOperationHistoryRepository> _operationHistoryRepository;
        private readonly OperationHistoryService _operationHistoryService;
        public OperationHistoryServiceTests()
        {
            _operationHistoryRepository = new Mock<IOperationHistoryRepository>();
            _operationHistoryService = new OperationHistoryService(_operationHistoryRepository.Object);
        }
        [Fact]
        public async Task GetOperations_WhenCalled_ReturnsOperations()
        {
            //Arrange
            var accountId = 1;
            var startDate = new DateTime(2021, 1, 1);
            var endDate = new DateTime(2021, 12, 31);
            var operations = new List<Operation>
            {
                new Operation
                {
                    AccountId = 1,
                    AccountType = AccountType.Checking,
                    OperationType = OperationType.Deposit,
                    OperationDate = new DateTime(2021, 1, 1),
                    Amount = 1000
                },
                new Operation
                {
                    AccountId = 1,
                    AccountType = AccountType.Checking,
                    OperationType = OperationType.Withdrawal,
                    OperationDate = new DateTime(2021, 1, 1),
                    Amount = 100
                }
            };
            _operationHistoryRepository.Setup(x => x.GetOperations(accountId, startDate, endDate)).ReturnsAsync(operations);
            //Act
            var result = await _operationHistoryService.GetOperations(accountId, startDate, endDate);
            //Assert
            Assert.Equal(operations, result);
        }   
          
    }
}
