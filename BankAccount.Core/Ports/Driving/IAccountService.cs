// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

namespace BankAccount.Core.Ports.Driving
{
    internal interface IAccountService <T>
    {
        Task<T> Deposit(T account, decimal amount);
        Task<T> Withdraw(T account, decimal amount);
    }
}
