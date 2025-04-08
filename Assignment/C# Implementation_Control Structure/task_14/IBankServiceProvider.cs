using System;
using System.Collections.Generic;

namespace Banking_System
{
    public interface IBankServiceProvider : ICustomerServiceProvider
    {
        void create_account(long accNo, string accType, float balance, task10_Customer customer);
        task10_Account[] listAccounts();
        void calculateInterest();
        List<Transaction> getTransations(long accNo, DateTime from, DateTime to);

    }
}
