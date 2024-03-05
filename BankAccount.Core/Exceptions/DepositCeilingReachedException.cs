// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

namespace BankAccount.Core
{
    public class DepositCeilingReachedException : Exception
    {
        public DepositCeilingReachedException()
        {
                
        }
        public DepositCeilingReachedException(string message) : base(message)
        {

        }
        public DepositCeilingReachedException(string message, Exception inner) : base(message, inner)
        {
            
        }
    }
}
