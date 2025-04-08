using System;
using System.Collections.Generic;

namespace Banking_System
{
    public class MapBasedBank : IBankServiceProvider
    {
        private Dictionary<long, AbstractAccount> accounts = new Dictionary<long, AbstractAccount>();

        public void create_account(long accNo, string accType, float balance, task10_Customer customer)
        {
            if (accounts.ContainsKey(accNo))
            {
                Console.WriteLine("Account number already exists. Cannot create duplicate.");
                return;
            }

            AbstractAccount account;

            if (accType.Equals("Savings", StringComparison.OrdinalIgnoreCase))
                account = new SavingsAccount(balance, 0.04f, customer);
            else if (accType.Equals("Current", StringComparison.OrdinalIgnoreCase))
                account = new CurrentAccount(balance, 5000, customer);
            else if (accType.Equals("ZeroBalance", StringComparison.OrdinalIgnoreCase))
                account = new ZeroBalanceAccount(customer);
            else
                throw new ArgumentException("Invalid account type.");

            account.SetAccountNumber(accNo); 

            accounts[accNo] = account;

            Console.WriteLine("Account added to Dictionary successfully.");
        }

        public task10_Account[] listAccounts()
        {
            List<task10_Account> result = new List<task10_Account>();
            foreach (var acc in accounts.Values)
            {
                result.Add(new task10_Account
                {
                    AccountNumber = acc.AccountNumber,
                    AccountType = acc.GetType().Name,
                    Balance = acc.Balance,
                    Customer = acc.Customer
                });
            }

            return result.ToArray();
        }

        public void calculateInterest()
        {
            foreach (var acc in accounts.Values)
            {
                if (acc is SavingsAccount sa)
                {
                    float interest = sa.Balance * sa.InterestRate;
                    sa.Balance += interest;
                    Console.WriteLine($"Interest of ₹{interest} added to account {sa.AccountNumber}");
                }
            }
        }
    }
}
