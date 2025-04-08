using System;

namespace Banking_System
{
    public class InvalidAccountException : Exception
    {
        public InvalidAccountException(string message) : base(message) { }
    }
}
