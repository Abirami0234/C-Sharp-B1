using System;
using System.Collections.Generic;
using System.Linq;

namespace Banking_System
{
    public class MapBasedBank : IBankServiceProvider
    {
        private Dictionary<long, AbstractAccount> accountMap = new();
        private List<Transaction> transactionList = new();

        public void create_account(long accNo, string accType, float balance, task10_Customer customer)
        {
            if (accountMap.ContainsKey(accNo))
            {
                Console.WriteLine("Account already exists.");
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
            accountMap[accNo] = account;
            Console.WriteLine("Account created successfully using Map.");
        }

        public float get_account_balance(long accNo)
        {
            if (!accountMap.ContainsKey(accNo))
                throw new InvalidAccountException("Account not found.");
            return accountMap[accNo].Balance;
        }

        public float deposit(long accNo, float amount)
        {
            if (!accountMap.ContainsKey(accNo))
                throw new InvalidAccountException("Account not found.");
            accountMap[accNo].Balance += amount;
            return accountMap[accNo].Balance;
        }

        public float withdraw(long accNo, float amount)
        {
            if (!accountMap.ContainsKey(accNo))
                throw new InvalidAccountException("Account not found.");

            var acc = accountMap[accNo];

            if (acc is CurrentAccount ca)
            {
                if (amount > ca.Balance + ca.OverdraftLimit)
                    throw new OverDraftLimitExceededException("Overdraft limit exceeded.");
            }
            else if (acc is SavingsAccount && acc.Balance - amount < 500)
                throw new InsufficientFundException("Min ₹500 balance required.");
            else if (amount > acc.Balance)
                throw new InsufficientFundException("Not enough balance.");

            acc.Balance -= amount;
            return acc.Balance;
        }

        public void transfer(long fromAcc, long toAcc, float amount)
        {
            withdraw(fromAcc, amount);
            deposit(toAcc, amount);
            Console.WriteLine($"₹{amount} transferred from A/C {fromAcc} to A/C {toAcc}");
        }

        public task10_Account getAccountDetails(long accNo)
        {
            if (!accountMap.ContainsKey(accNo))
                throw new InvalidAccountException("Account not found.");

            var acc = accountMap[accNo];
            return new task10_Account
            {
                AccountNumber = acc.AccountNumber,
                AccountType = acc.GetType().Name,
                Balance = acc.Balance,
                Customer = acc.Customer
            };
        }

        public task10_Account[] listAccounts()
        {
            return accountMap.Values.Select(acc => new task10_Account
            {
                AccountNumber = acc.AccountNumber,
                AccountType = acc.GetType().Name,
                Balance = acc.Balance,
                Customer = acc.Customer
            }).ToArray();
        }

        public void calculateInterest()
        {
            foreach (var acc in accountMap.Values)
            {
                if (acc is SavingsAccount sa)
                {
                    float interest = sa.Balance * sa.InterestRate;
                    sa.Balance += interest;
                    Console.WriteLine($"Interest ₹{interest} added to A/C {sa.AccountNumber}");
                }
            }
        }

        public List<Transaction> getTransations(long accNo, DateTime fromDate, DateTime toDate)
        {
            Console.WriteLine("Transactions not available in MapBasedBank.");
            return new List<Transaction>();
        }
    }
}
