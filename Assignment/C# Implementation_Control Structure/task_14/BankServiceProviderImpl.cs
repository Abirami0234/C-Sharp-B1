using System;
using System.Collections.Generic;

namespace Banking_System
{
    public class BankServiceProviderImpl : CustomerServiceProviderImpl, IBankServiceProvider
    {
        private string branchName;
        private string branchAddress;

        public BankServiceProviderImpl(string branchName, string branchAddress)
        {
            this.branchName = branchName;
            this.branchAddress = branchAddress;
        }

        public void create_account(long accNo, string accType, float balance, task10_Customer customer)
        {
            AbstractAccount newAccount = null;

            if (accType.Equals("Savings", StringComparison.OrdinalIgnoreCase))
            {
                if (balance < 500)
                    throw new Exception("Minimum balance for Savings Account must be ₹500.");

                newAccount = new SavingsAccount(balance, 0.04f, customer);
                 // Assuming 4% interest
            }
            else if (accType.Equals("Current", StringComparison.OrdinalIgnoreCase))
            {
                newAccount = new CurrentAccount(balance, 10000, customer); 
            }
            else if (accType.Equals("ZeroBalance", StringComparison.OrdinalIgnoreCase))
            {
                if (balance != 0)
                    throw new Exception("Zero Balance account must be created with zero balance.");

                newAccount = new ZeroBalanceAccount(customer);
            }
            else
            {
                throw new Exception("Invalid account type.");
            }

            accountList.Add(newAccount.AccountNumber, newAccount);
            Console.WriteLine($"\nAccount created successfully!\nAccount Number: {newAccount.AccountNumber}");
        }

        public task10_Account[] listAccounts()
        {
            List<task10_Account> task10Accounts = new List<task10_Account>();

            foreach (var acc in CustomerServiceProviderImpl.accountList)
            {
                var abstractAcc = acc.Value;
                var customer = abstractAcc.Customer;

                task10_Account task10Acc = new task10_Account
                {
                    AccountNumber = acc.Key,
                    AccountType = abstractAcc.GetType().Name,
                    Balance = abstractAcc.Balance,
                    Customer = customer
                };

                task10Accounts.Add(task10Acc);
            }

            return task10Accounts.ToArray(); 
        }


        public void calculateInterest()
        {
            foreach (var acc in accountList.Values)
            {
                if (acc is SavingsAccount savings)
                {
                    float interest = savings.Balance * savings.InterestRate;
                    savings.Balance += interest;
                    Console.WriteLine($"Interest ₹{interest:F2} added to A/C {savings.AccountNumber}. New Balance: ₹{savings.Balance:F2}");
                }
            }
        }
        private List<Transaction> transactionList = new List<Transaction>();

        public List<Transaction> getTransations(long accNo, DateTime from, DateTime to)
        {
            return transactionList
                .Where(t => t.AccountNumber == accNo && t.TransactionDate >= from && t.TransactionDate <= to)
                .ToList();
        }



        public void PrintBranchInfo()
        {
            Console.WriteLine($"\nBranch Name: {branchName}");
            Console.WriteLine($"Branch Address: {branchAddress}");
        }

    }
}
