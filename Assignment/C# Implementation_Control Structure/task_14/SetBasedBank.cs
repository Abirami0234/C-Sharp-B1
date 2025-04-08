using System;
using System.Collections.Generic;
using System.Linq;

namespace Banking_System
{
        public class SetBasedBank : IBankServiceProvider
        {
            private HashSet<AbstractAccount> accountSet = new(new AccountComparer());

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

                if (!accountSet.Add(account))
                    Console.WriteLine("Account already exists.");
                else
                    Console.WriteLine("Account created successfully using Set.");
            }

            public float get_account_balance(long accNo)
            {
                var acc = FindAccount(accNo);
                return acc.Balance;
            }

            public float deposit(long accNo, float amount)
            {
                var acc = FindAccount(accNo);
                acc.Balance += amount;
                return acc.Balance;
            }

            public float withdraw(long accNo, float amount)
            {
                var acc = FindAccount(accNo);

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
                var acc = FindAccount(accNo);
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
                return accountSet
                    .OrderBy(a => a.Customer.FirstName + a.Customer.LastName)
                    .Select(acc => new task10_Account
                    {
                        AccountNumber = acc.AccountNumber,
                        AccountType = acc.GetType().Name,
                        Balance = acc.Balance,
                        Customer = acc.Customer
                    }).ToArray();
            }

            public void calculateInterest()
            {
                foreach (var acc in accountSet)
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
                Console.WriteLine("Transactions not available in SetBasedBank.");
                return new List<Transaction>();
            }

            private AbstractAccount FindAccount(long accNo)
            {
                foreach (var acc in accountSet)
                {
                    if (acc.AccountNumber == accNo)
                        return acc;
                }
                throw new InvalidAccountException("Account not found.");
            }

            class AccountComparer : IEqualityComparer<AbstractAccount>
            {
                public bool Equals(AbstractAccount x, AbstractAccount y)
                {
                    return x.AccountNumber == y.AccountNumber;
                }

                public int GetHashCode(AbstractAccount obj)
                {
                    return obj.AccountNumber.GetHashCode();
                }
            }
        }
}

