using System;
using System.Collections.Generic;

namespace Banking_System
{
    public class Bank
    {
        private Dictionary<long, task10_Account> accounts = new Dictionary<long, task10_Account>();
        private long nextAccountNumber = 1001;

        public long CreateAccount(task10_Customer customer, string accType, float balance)
        {
            long accNo = nextAccountNumber++;
            task10_Account account = new task10_Account(accNo, accType, balance, customer);
            accounts[accNo] = account;
            Console.WriteLine($"Account created successfully. Account Number: {accNo}");
            return accNo;
        }

        public float GetAccountBalance(long accNo)
        {
            if (accounts.ContainsKey(accNo))
                return accounts[accNo].Balance;
            else
                throw new Exception("Account not found.");
        }

        public float Deposit(long accNo, float amount)
        {
            if (accounts.ContainsKey(accNo))
            {
                accounts[accNo].Balance += amount;
                return accounts[accNo].Balance;
            }
            throw new Exception("Account not found.");
        }

        public float Withdraw(long accNo, float amount)
        {
            if (accounts.ContainsKey(accNo))
            {
                if (accounts[accNo].Balance >= amount)
                {
                    accounts[accNo].Balance -= amount;
                    return accounts[accNo].Balance;
                }
                else
                {
                    throw new Exception("Insufficient balance.");
                }
            }
            throw new Exception("Account not found.");
        }

        public void Transfer(long fromAccNo, long toAccNo, float amount)
        {
            if (accounts.ContainsKey(fromAccNo) && accounts.ContainsKey(toAccNo))
            {
                if (accounts[fromAccNo].Balance >= amount)
                {
                    accounts[fromAccNo].Balance -= amount;
                    accounts[toAccNo].Balance += amount;
                    Console.WriteLine($"Transferred ₹{amount} from {fromAccNo} to {toAccNo}.");
                }
                else
                {
                    throw new Exception("Insufficient balance for transfer.");
                }
            }
            else
            {
                throw new Exception("Invalid account number(s).");
            }
        }

        public void GetAccountDetails(long accNo)
        {
            if (accounts.ContainsKey(accNo))
            {
                accounts[accNo].PrintAccountInfo();
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }
    }
}
