using System;

namespace Banking_System
{
    public class Transaction
    {
        public long AccountNumber { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; } // Withdraw, Deposit, Transfer
        public float TransactionAmount { get; set; }

        public Transaction() { }

        public Transaction(long accNo, string desc, DateTime date, string type, float amount)
        {
            AccountNumber = accNo;
            Description = desc;
            TransactionDate = date;
            TransactionType = type;
            TransactionAmount = amount;
        }

        public void PrintTransaction()
        {
            Console.WriteLine($"Date: {TransactionDate}, Type: {TransactionType}, Amount: ₹{TransactionAmount}, Description: {Description}");
        }
    }
}
