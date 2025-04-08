namespace Banking_System
{
    public class SavingsAccount : AbstractAccount
    {
        public float InterestRate { get; set; }
        private const float MinimumBalance = 500;

        public SavingsAccount(float balance, float interestRate, task10_Customer customer)
            : base("Savings", balance < MinimumBalance ? MinimumBalance : balance, customer)
        {
            InterestRate = interestRate;
        }

        public override void Withdraw(float amount)
        {
            if (Balance - amount >= MinimumBalance)
                Balance -= amount;
            else
                throw new Exception("Insufficient balance. Minimum balance must be maintained.");
        }
    }
}
