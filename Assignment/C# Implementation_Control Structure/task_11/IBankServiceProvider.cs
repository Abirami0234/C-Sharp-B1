namespace Banking_System
{
    public interface IBankServiceProvider
    {
        void create_account(long accNo, string accType, float balance, task10_Customer customer);
        task10_Account[] listAccounts(); 
        void calculateInterest();
    }
}
