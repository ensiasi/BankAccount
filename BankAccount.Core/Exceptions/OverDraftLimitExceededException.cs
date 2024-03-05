// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using System.Runtime.Serialization;

namespace BankAccount.Core
{
    [Serializable]
    public class OverDraftLimitExceededException : Exception
    {
        public OverDraftLimitExceededException()
        {
        }

        public OverDraftLimitExceededException(string? message) : base(message)
        {
        }

        public OverDraftLimitExceededException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected OverDraftLimitExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
