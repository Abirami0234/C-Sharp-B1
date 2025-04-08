namespace Banking_System
{
    public class ZeroBalanceAccount : AbstractAccount
    {
        public ZeroBalanceAccount(task10_Customer customer)
            : base("ZeroBalance", 0, customer) { }

        public override void Withdraw(float amount)
        {
            if (Balance >= amount)
                Balance -= amount;
            else
                throw new Exception("Insufficient balance.");
        }
    }
}
