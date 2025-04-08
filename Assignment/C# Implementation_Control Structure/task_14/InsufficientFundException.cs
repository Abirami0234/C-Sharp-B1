using System;

namespace Banking_System
{
    public class InsufficientFundException : Exception
    {
        public InsufficientFundException(string message) : base(message) { }
    }
}
