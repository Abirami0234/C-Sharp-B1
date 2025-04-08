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

            account.SetAccountNumber(accNo); // Allow setting acc number manually for collection-based banks

            accountList.Add(account);
            Console.WriteLine("Account created successfully using List.");
        }

        public float get_account_balance(long accNo)
        {
            var acc = accountList.FirstOrDefault(a => a.AccountNumber == accNo);
            if (acc == null) throw new InvalidAccountException("Account not found.");
            return acc.Balance;
        }

        public float deposit(long accNo, float amount)
        {
            var acc = accountList.FirstOrDefault(a => a.AccountNumber == accNo);
            if (acc == null) throw new InvalidAccountException("Account not found.");
            acc.Balance += amount;
            return acc.Balance;
        }

        public float withdraw(long accNo, float amount)
        {
            var acc = accountList.FirstOrDefault(a => a.AccountNumber == accNo);
            if (acc == null) throw new InvalidAccountException("Account not found.");

            if (acc is CurrentAccount currentAcc)
            {
                if (amount > currentAcc.Balance + currentAcc.OverdraftLimit)
                    throw new OverDraftLimitExceededException("Exceeds overdraft limit.");
            }
            else if (acc is SavingsAccount && acc.Balance - amount < 500)
            {
                throw new InsufficientFundException("Savings account must maintain a minimum balance of ₹500.");
            }
            else if (amount > acc.Balance)
            {
                throw new InsufficientFundException("Not enough balance.");
            }

            acc.Balance -= amount;
            return acc.Balance;
        }

        public void transfer(long fromAcc, long toAcc, float amount)
        {
            if (fromAcc == toAcc) throw new Exception("Cannot transfer to the same account.");

            withdraw(fromAcc, amount);
            deposit(toAcc, amount);
            Console.WriteLine($"Transferred ₹{amount} from A/C {fromAcc} to A/C {toAcc}");
        }

        public task10_Account getAccountDetails(long accNo)
        {
            var acc = accountList.FirstOrDefault(a => a.AccountNumber == accNo);
            if (acc == null) throw new InvalidAccountException("Account not found.");

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
            return accountList.Select(acc => new task10_Account
            {
                AccountNumber = acc.AccountNumber,
                AccountType = acc.GetType().Name,
                Balance = acc.Balance,
                Customer = acc.Customer
            }).ToArray();
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

        public List<Transaction> getTransations(long accNo, DateTime fromDate, DateTime toDate)
        {
            // Placeholder for DB-supported feature; currently returns empty list
            Console.WriteLine("Transaction history not supported in ListBasedBank.");
            return new List<Transaction>();
        }
    }
}
