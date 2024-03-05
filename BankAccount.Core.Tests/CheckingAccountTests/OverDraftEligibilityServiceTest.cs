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
        public void IsOverDraftEligible_WhenAccountIsOverDraftEnabled_ReturnsTrue()
        {
            //Arrange
            var account = new CheckingAccount
            {
                IsOverDraftEnabled = true
            };
            //Act
            var result = _overDraftEligibilityService.IsOverDraftEligible(account);
            //Assert
            Assert.True(result);
        }
        [Fact]
        public void IsOverDraftEligible_WhenAccountIsOverDraftDisabled_ReturnsFalse()
        {
            //Arrange
            var account = new CheckingAccount
            {
                IsOverDraftEnabled = false
            };
            //Act
            var result = _overDraftEligibilityService.IsOverDraftEligible(account);
            //Assert
            Assert.False(result);
        }
        [Fact]
        public void IsDraftLimitExceeded_WhenAccountIsNotOverDraftEnabled_ReturnsFalse()
        {
            //Arrange
            var account = new CheckingAccount
            {
                IsOverDraftEnabled = false
            };
            //Act
            var result = _overDraftEligibilityService.IsDraftLimitExceeded(account, 0);
            //Assert
            Assert.False(result);
        }
        [Fact]
        public void IsDraftLimitExceeded_WhenBalanceAfterWithdrawIsGreaterThanOverDraftLimit_ReturnsFalse()
        {
            //Arrange
            var account = new CheckingAccount
            {
                IsOverDraftEnabled = true,
                OverDraftLimit = 100
            };
            //Act
            var result = _overDraftEligibilityService.IsDraftLimitExceeded(account, -200);
            //Assert
            Assert.False(result);
        }
        [Fact]
        public void IsDraftLimitExceeded_WhenBalanceAfterWithdrawIsLessThanOverDraftLimit_ReturnsTrue()
        {
            //Arrange
            var account = new CheckingAccount
            {
                IsOverDraftEnabled = true,
                OverDraftLimit = 100
            };
            //Act
            var result = _overDraftEligibilityService.IsDraftLimitExceeded(account, -50);
            //Assert
            Assert.True(result);
        }

    }
}
