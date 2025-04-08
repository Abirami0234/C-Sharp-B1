using System;
using System.Collections.Generic;

namespace Banking_System
{
    public class ExceptionHandling : BankServiceProviderImpl
    {
        public ExceptionHandling() : base("DefaultBranch", "DefaultAddress")
        { }
        public float Withdraw(long accNo, float amount)
        {
            if (!CustomerServiceProviderImpl.accountList.ContainsKey(accNo))
                throw new InvalidAccountException("Account number does not exist.");

            var account = CustomerServiceProviderImpl.accountList[accNo];

            if (account is CurrentAccount currentAcc)
            {
                if (amount > currentAcc.Balance + currentAcc.OverdraftLimit)
                    throw new OverDraftLimitExceededException("Exceeds overdraft limit.");
            }
            else if (account is SavingsAccount && account.Balance - amount < 500)
            {
                throw new InsufficientFundException("Savings account must maintain a minimum balance of ₹500.");
            }
            else if (amount > account.Balance)
            {
                throw new InsufficientFundException("Not enough balance.");
            }

            account.Balance -= amount;
            return account.Balance;
        }

        public float Deposit(long accNo, float amount)
        {
            if (!CustomerServiceProviderImpl.accountList.ContainsKey(accNo))
                throw new InvalidAccountException("Account number does not exist.");

            var account = CustomerServiceProviderImpl.accountList[accNo];
            account.Balance += amount;
            return account.Balance;
        }

        public void Transfer(long fromAcc, long toAcc, float amount)
        {
            if (!CustomerServiceProviderImpl.accountList.ContainsKey(fromAcc) ||
                !CustomerServiceProviderImpl.accountList.ContainsKey(toAcc))
                throw new InvalidAccountException("One or both account numbers are invalid.");

            Withdraw(fromAcc, amount);
            Deposit(toAcc, amount);

            Console.WriteLine($"Transferred ₹{amount} from A/C {fromAcc} to A/C {toAcc}");
        }

        public void GetAccountDetails(long accNo)
        {
            if (!CustomerServiceProviderImpl.accountList.ContainsKey(accNo))
                throw new InvalidAccountException("Account number not found.");

            var acc = CustomerServiceProviderImpl.accountList[accNo];
            acc.PrintDetails();
        }
        
    }
}
