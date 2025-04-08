using System;

namespace Banking_System
{
    public interface ICustomerServiceProvider
    {
        float get_account_balance(long account_number);
        float deposit(long account_number, float amount);
        float withdraw(long account_number, float amount);
        void transfer(long from_account_number, long to_account_number, float amount);
        void getAccountDetails(long account_number);
    }
}
