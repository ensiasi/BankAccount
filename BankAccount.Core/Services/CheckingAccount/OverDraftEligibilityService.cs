using BankAccount.Core.Exceptions;
using BankAccount.Core.Models;
using BankAccount.Core.Ports.Driving;

namespace BankAccount.Core.Services.CheckingAccounts
{
    public class OverDraftEligibilityService : IOverDraftEligibilityService
    {
        public void CheckOverDraft(CheckingAccount account, decimal amount)
        {
            var newBalance = account.Balance - amount;
            if (newBalance < 0)
            {
                if ((!account.IsOverDraftEnabled))
                {
                    throw new InsufficientFundsException("Insufficient balance for withdrawal");
                }
                else if (Math.Abs(newBalance) > account.OverDraftLimit)
                {
                    throw new OverDraftLimitExceededException("Overdraft limit exceeded");
                }
            }
        }
    }
}
