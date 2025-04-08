using System;

namespace Banking_System
{
    public abstract class AbstractAccount
    {
        private static long lastAccNo = 1000;

        public long AccountNumber { get; protected set; }
        public string AccountType { get; set; }
        public float Balance { get; set; }
        public task10_Customer Customer { get; set; }

        public AbstractAccount(string type, float balance, task10_Customer customer)
        {
            AccountNumber = ++lastAccNo;
            AccountType = type;
            Balance = balance;
            Customer = customer;
        }

        public abstract void Withdraw(float amount);

        public virtual void Deposit(float amount)
        {
            Balance += amount;
        }

        public void PrintDetails()
        {
            Console.WriteLine($"\nAccount Number: {AccountNumber}, Type: {AccountType}, Balance: {Balance}");
            Customer.PrintCustomerInfo();
        }
    }
}
