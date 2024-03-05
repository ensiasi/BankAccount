// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

namespace BankAccount.Core.Ports.Driving
{
    internal interface IAccountService <T>
    {
        Task<T> Deposit(string accountNumber, decimal amount);
        Task<T> Withdraw(string accountNumber, decimal amount);
    }
}
