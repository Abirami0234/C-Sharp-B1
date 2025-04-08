using System;
using System.Collections.Generic;

namespace Banking_System
{
    public interface IBankRepository
    {
        void createAccount(task10_Customer customer, long accNo, string accType, float balance);

        List<task10_Account> listAccounts();

        void calculateInterest();

        float getAccountBalance(long accountNumber);

        float deposit(long accountNumber, float amount);

        float withdraw(long accountNumber, float amount);

        void transfer(long fromAccountNumber, long toAccountNumber, float amount);

        task10_Account getAccountDetails(long accountNumber);

        List<Transaction> getTransactions(long accountNumber, DateTime fromDate, DateTime toDate);
    }
}
