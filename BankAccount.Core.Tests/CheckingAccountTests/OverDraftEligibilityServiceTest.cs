using BankAccount.Core.Exceptions;
using BankAccount.Core.Models;
using BankAccount.Core.Ports.Driving;
using BankAccount.Core.Services.CheckingAccounts;
using Xunit;

namespace BankAccount.Core.Tests.CheckingAccountTests
{
    public class OverDraftEligibilityServiceTest
    {
        private readonly IOverDraftEligibilityService _overDraftEligibilityService;
        public OverDraftEligibilityServiceTest()
        {
            _overDraftEligibilityService = new OverDraftEligibilityService();

        }
        [Fact]
        public void IsOverDraftEligible_WhenAccountIsNotOverDraftEnabled_Throw_InsufficientFundsException()
        {
            //Arrange
            var account = new CheckingAccount
            {
                IsOverDraftEnabled = false
            };
            //Act
            var exception = Assert.Throws<InsufficientFundsException>(() => _overDraftEligibilityService.CheckOverDraft(account, 100));
            //Assert
            Assert.Equal("Insufficient balance for withdrawal", exception.Message);
        }
        [Fact]
        public void IsOverDraftEligible_WhenAccountOverDraftLimitIsReached_Throw_OverDraftLimitExceededException()
        {
            //Arrange
            var account = new CheckingAccount
            {
                IsOverDraftEnabled = true,
                OverDraftLimit = 100,
                Balance=0
                
            };
            //Act
            var exception = Assert.Throws<OverDraftLimitExceededException>(() => _overDraftEligibilityService.CheckOverDraft(account, 150));
            //Assert
            Assert.Equal("Overdraft limit exceeded", exception.Message);
        }
    }
}
