namespace Banking_System
{
    public class CurrentAccount : AbstractAccount
    {
        public float OverdraftLimit { get; set; }

        public CurrentAccount(float balance, float overdraftLimit, task10_Customer customer)
            : base("Current", balance, customer)
        {
            OverdraftLimit = overdraftLimit;
        }

        public override void Withdraw(float amount)
        {
            if (Balance - amount >= -OverdraftLimit)
                Balance -= amount;
            else
                throw new Exception("Overdraft limit exceeded.");
        }
    }
}
