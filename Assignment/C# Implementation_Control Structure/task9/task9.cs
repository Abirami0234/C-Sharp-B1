using System;

namespace Banking_System
{
    abstract class BankAccount
    {
        public long AccountNumber { get; set; }
        public string CustomerName { get; set; }
        public float Balance { get; set; }

        public BankAccount() { }

        public BankAccount(long accNo, string name, float balance)
        {
            AccountNumber = accNo;
            CustomerName = name;
            Balance = balance;
        }

        public void PrintAccountInfo()
        {
            Console.WriteLine($"Account Number: {AccountNumber}\nCustomer Name: {CustomerName}\nBalance: {Balance}");
        }

        public abstract void Deposit(float amount);
        public abstract void Withdraw(float amount);
        public abstract void CalculateInterest();
    }

    class SavingsAccount : BankAccount
    {
        public float InterestRate { get; set; }

        public SavingsAccount(long accNo, string name, float balance, float interestRate)
            : base(accNo, name, balance)
        {
            InterestRate = interestRate;
        }

        public override void Deposit(float amount)
        {
            Balance += amount;
            Console.WriteLine($"Deposited: {amount}, New Balance: {Balance}");
        }

        public override void Withdraw(float amount)
        {
            if (amount <= Balance)
            {
                Balance -= amount;
                Console.WriteLine($"Withdrawn: {amount}, New Balance: {Balance}");
            }
            else
            {
                Console.WriteLine("Insufficient balance.");
            }
        }

        public override void CalculateInterest()
        {
            float interest = Balance * InterestRate / 100;
            Balance += interest;
            Console.WriteLine($"Interest Added: {interest}, New Balance: {Balance}");
        }
    }

    class CurrentAccount : BankAccount
    {
        private const float OverdraftLimit = 5000;

        public CurrentAccount(long accNo, string name, float balance) : base(accNo, name, balance) { }

        public override void Deposit(float amount)
        {
            Balance += amount;
            Console.WriteLine($"Deposited: {amount}, New Balance: {Balance}");
        }

        public override void Withdraw(float amount)
        {
            if (Balance - amount >= -OverdraftLimit)
            {
                Balance -= amount;
                Console.WriteLine($"Withdrawn: {amount}, New Balance: {Balance}");
            }
            else
            {
                Console.WriteLine("Overdraft limit exceeded.");
            }
        }

        public override void CalculateInterest()
        {
            Console.WriteLine("Current accounts do not earn interest.");
        }
    }

    class Task9
    {
        static void Main(string[] args)
        {
            long accNo = 1001;

            Console.WriteLine("Choose Account Type:\n1. SavingsAccount\n2. CurrentAccount");
            int type = int.Parse(Console.ReadLine());

            Console.Write("Enter Customer Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Initial Balance: ");
            float balance = float.Parse(Console.ReadLine());

            BankAccount account;

            if (type == 1)
            {
                Console.Write("Enter Interest Rate: ");
                float rate = float.Parse(Console.ReadLine());
                account = new SavingsAccount(accNo, name, balance, rate);
            }
            else
            {
                account = new CurrentAccount(accNo, name, balance);
            }

            string input;
            do
            {
                Console.WriteLine("\nOptions: deposit, withdraw, calculate_interest, info, exit");
                input = Console.ReadLine();

                switch (input)
                {
                    case "deposit":
                        Console.Write("Enter amount to deposit: ");
                        float damount = float.Parse(Console.ReadLine());
                        account.Deposit(damount);
                        break;
                    case "withdraw":
                        Console.Write("Enter amount to withdraw: ");
                        float wamount = float.Parse(Console.ReadLine());
                        account.Withdraw(wamount);
                        break;
                    case "calculate_interest":
                        account.CalculateInterest();
                        break;
                    case "info":
                        account.PrintAccountInfo();
                        break;
                }
            } while (input != "exit");
        }
    }

}
