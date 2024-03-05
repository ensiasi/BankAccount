using BankAccount.Core.Models;

namespace BankAccount.Core.Ports.Driving
{
    public interface IOverDraftEligibilityService
    {
        public bool IsDraftLimitExceeded(CheckingAccount account, decimal balanceAfterWithDraw);
        public bool IsOverDraftEligible(CheckingAccount account);
    }
}
