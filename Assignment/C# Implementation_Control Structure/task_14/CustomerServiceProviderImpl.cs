using System;
using System.Collections.Generic;

namespace Banking_System
{
    public class CustomerServiceProviderImpl : ICustomerServiceProvider
    {
        protected static Dictionary<long, AbstractAccount> accountList = new Dictionary<long, AbstractAccount>();

        public float get_account_balance(long account_number)
        {
            if (accountList.ContainsKey(account_number))
                return accountList[account_number].Balance;
            else
                throw new Exception("Account not found.");
        }

        public float deposit(long account_number, float amount)
        {
            if (accountList.ContainsKey(account_number))
            {
                accountList[account_number].Balance += amount;
                return accountList[account_number].Balance;
            }
            else
                throw new Exception("Account not found.");
        }

        public float withdraw(long account_number, float amount)
        {
            if (!accountList.ContainsKey(account_number))
                throw new Exception("Account not found.");

            AbstractAccount account = accountList[account_number];

            if (account is SavingsAccount savings)
            {
                if (savings.Balance - amount >= 500)
                {
                    savings.Balance -= amount;
                    return savings.Balance;
                }
                else
                {
                    throw new Exception("Minimum balance of ₹500 must be maintained.");
                }
            }
            else if (account is CurrentAccount current)
            {
                if (current.Balance + current.OverdraftLimit >= amount)
                {
                    current.Balance -= amount;
                    return current.Balance;
                }
                else
                {
                    throw new Exception("Withdrawal amount exceeds overdraft limit.");
                }
            }
            else if (account is ZeroBalanceAccount zero)
            {
                if (zero.Balance >= amount)
                {
                    zero.Balance -= amount;
                    return zero.Balance;
                }
                else
                {
                    throw new Exception("Insufficient balance.");
                }
            }
            else
            {
                throw new Exception("Unsupported account type.");
            }
        }

        public void transfer(long from_account_number, long to_account_number, float amount)
        {
            try
            {
                if (!accountList.ContainsKey(from_account_number) || !accountList.ContainsKey(to_account_number))
                    throw new Exception("One or both account numbers are invalid.");

                
                float withdrawn = withdraw(from_account_number, amount);

                
                float newBalance = deposit(to_account_number, amount);

                Console.WriteLine($"Transferred ₹{amount} from A/C {from_account_number} to A/C {to_account_number}.");
                Console.WriteLine($"New balance: From A/C ₹{accountList[from_account_number].Balance}, To A/C ₹{newBalance}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Transfer failed: {ex.Message}");
            }
        }


        public task10_Account getAccountDetails(long accNo)
        {
            if (!accountList.ContainsKey(accNo))
                throw new InvalidAccountException("Account number not found");

            AbstractAccount acc = accountList[accNo];
            return new task10_Account(acc.AccountNumber, acc.GetType().Name, acc.Balance, acc.Customer);
        }

    }


}
