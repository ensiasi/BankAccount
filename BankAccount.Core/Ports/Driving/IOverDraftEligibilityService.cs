using BankAccount.Core.Models;

namespace BankAccount.Core.Ports.Driving
{
    public interface IOverDraftEligibilityService
    {
        void CheckOverDraft(CheckingAccount account, decimal amount);
    }
}
