using System;
using System.Collections.Generic;
using System.Linq;

namespace Banking_System
{
    public class ListBasedBank : IBankServiceProvider
    {
        private List<AbstractAccount> accountList = new List<AbstractAccount>();

        public void create_account(long accNo, string accType, float balance, task10_Customer customer)
        {
            
            if (accountList.Any(acc => acc.AccountNumber == accNo))
            {
                Console.WriteLine("Account already exists with this account number.");
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

            accountList.Add(account);
            Console.WriteLine("Account created successfully using List.");
        }

        public task10_Account[] listAccounts()
        {
            List<task10_Account> result = new List<task10_Account>();

            foreach (var acc in accountList)
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
            foreach (var acc in accountList)
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
