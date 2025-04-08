namespace Banking_System
{
    public static class AccountExtensions
    {
        public static task10_Account ToTask10Account(this AbstractAccount acc)
        {
            return new task10_Account(acc.AccountNumber, acc.GetType().Name, acc.Balance, acc.Customer);
        }
    }
}
