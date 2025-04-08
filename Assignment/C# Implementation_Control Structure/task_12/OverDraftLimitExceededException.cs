using System;

namespace Banking_System
{
    public class OverDraftLimitExceededException : Exception
    {
        public OverDraftLimitExceededException(string message) : base(message) { }
    }
}
