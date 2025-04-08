using System;
using System.Collections.Generic;
using System.Linq;

namespace Banking_System
{
    // Custom comparer to check uniqueness by AccountNumber
    public class AccountComparer : IEqualityComparer<AbstractAccount>
    {
        public bool Equals(AbstractAccount? x, AbstractAccount? y)
        {
            if (x == null || y == null) return false;
            return x.AccountNumber == y.AccountNumber;
        }

        public int GetHashCode(AbstractAccount obj)
        {
            return obj.AccountNumber.GetHashCode();
        }
    }

    // For sorting based on customer name
    public class CustomerNameComparer : IComparer<AbstractAccount>
    {
        public int Compare(AbstractAccount? x, AbstractAccount? y)
        {
            if (x == null || y == null) return 0;
            return string.Compare(x.Customer.FirstName + x.Customer.LastName,
                                  y.Customer.FirstName + y.Customer.LastName,
                                  StringComparison.OrdinalIgnoreCase);
        }
    }

    public class SetBasedBank : IBankServiceProvider
    {
        private HashSet<AbstractAccount> accounts = new HashSet<AbstractAccount>(new AccountComparer());

        public void create_account(long accNo, string accType, float balance, task10_Customer customer)
        {
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

            if (accounts.Add(account))
                Console.WriteLine("Account added successfully to HashSet.");
            else
                Console.WriteLine("Duplicate account — not added.");
        }

        public task10_Account[] listAccounts()
        {
            List<AbstractAccount> sortedAccounts = accounts.ToList();
            sortedAccounts.Sort(new CustomerNameComparer());

            List<task10_Account> result = new List<task10_Account>();
            foreach (var acc in sortedAccounts)
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
            foreach (var acc in accounts)
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
