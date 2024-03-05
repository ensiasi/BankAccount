using BankAccount.Core.Models;
using BankAccount.Core.Ports.Driving;

namespace BankAccount.Core.Services.CheckingAccounts
{
    public class OverDraftEligibilityService : IOverDraftEligibilityService
    {
        public bool IsDraftLimitExceeded(CheckingAccount account, decimal balanceAfterWithDraw)
        {
            if (account.IsOverDraftEnabled)
            {
                if (Math.Abs(balanceAfterWithDraw) > account.OverDraftLimit)
                {
                    return false;
                }
                return true;
            }
            return false;
        }
        public bool IsOverDraftEligible(CheckingAccount account)
        {
            if (account.IsOverDraftEnabled)
            {
                return true;
            }
            return false;
        }
    }
}
