using System;


namespace Banking_System
{
    class BankTasks
    {
        public void LoanEligibility()
        {
            Console.Write("Enter Credit Score: ");
            int creditScore = int.Parse(Console.ReadLine());
            Console.Write("Enter Annual Income: $ ");
            double income = double.Parse(Console.ReadLine());

            if (creditScore > 700 && income >= 50000)
                Console.WriteLine("Eligible for loan.");
            else
                Console.WriteLine("Not eligible for loan.");
        }

        public void ATMTransaction()
        {
            Console.Write("Enter current balance: $ ");
            double balance = double.Parse(Console.ReadLine());

            Console.WriteLine("Options: 1. Check Balance  2. Withdraw  3. Deposit");
            int option = int.Parse(Console.ReadLine());
            bool success = false;

            switch (option)
            {
                case 1:
                    Console.WriteLine($"Current Balance: ${balance}");
                    success = true;
                    break;
                case 2:
                    Console.Write("Enter amount to withdraw: $ ");
                    double withdraw = double.Parse(Console.ReadLine());
                    if (withdraw % 100 == 0 || withdraw % 500 == 0)
                    {
                        if (withdraw <= balance)
                        {
                            balance -= withdraw;
                            Console.WriteLine($"Withdrawal successful. New balance: ${balance}");
                            success = true;
                        }
                        else
                            Console.WriteLine("Insufficient balance.");
                    }
                    else
                        Console.WriteLine("Withdraw amount must be multiple of 100 or 500.");
                    break;
                case 3:
                    Console.Write("Enter amount to deposit: $ ");
                    double deposit = double.Parse(Console.ReadLine());
                    balance += deposit;
                    Console.WriteLine($"Deposit successful. New balance: ${balance}");
                    success = true;
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }

            Console.WriteLine(success ? "Transaction Status: Success" : "Transaction Status: Failed");
        }

        public void CompoundInterest()
        {
            Console.Write("Enter number of customers: ");
            int n = int.Parse(Console.ReadLine());

            for (int i = 1; i <= n; i++)
            {
                Console.WriteLine($"\nCustomer {i}:");
                Console.Write("Initial balance: $ ");
                double principal = double.Parse(Console.ReadLine());
                Console.Write("Annual interest rate (%): ");
                double rate = double.Parse(Console.ReadLine());
                Console.Write("Number of years: ");
                int years = int.Parse(Console.ReadLine());

                double futureBalance = principal * Math.Pow((1 + rate / 100), years);
                Console.WriteLine($"Future balance after {years} years: ${futureBalance:F2}");
            }
        }

        public void AccountBalanceCheck()
        {
            Dictionary<string, double> accounts = new Dictionary<string, double>()
            {
                {"1001", 1500.00},
                {"1002", 2700.50},
                {"1003", 820.75}
            };

            while (true)
            {
                Console.Write("Enter account number to check balance: ");
                string acc = Console.ReadLine();
                if (accounts.ContainsKey(acc))
                {
                    Console.WriteLine($"Account Balance: ${accounts[acc]}");
                    break;
                }
                else
                    Console.WriteLine("Invalid account number. Try again.");
            }
        }

        public void PasswordValidation()
        {
            Console.Write("Create a password: ");
            string password = Console.ReadLine();

            bool hasUpper = false, hasDigit = false, hasLength = password.Length >= 8;

            foreach (char ch in password)
            {
                if (char.IsUpper(ch)) hasUpper = true;
                if (char.IsDigit(ch)) hasDigit = true;
            }

            if (hasLength && hasUpper && hasDigit)
            {
                Console.WriteLine("Password is valid.");
            }
            else
            {
                Console.WriteLine("Invalid password.");
                if (!hasLength) Console.WriteLine("- Must be at least 8 characters.");
                if (!hasUpper) Console.WriteLine("- Must contain at least one uppercase letter.");
                if (!hasDigit) Console.WriteLine("- Must contain at least one digit.");
            }
        }

        public void TransactionHistory()
        {
            List<string> transactions = new List<string>();
            double balance = 0;

            while (true)
            {
                Console.WriteLine("Enter transaction type (deposit/withdraw/exit): ");
                string type = Console.ReadLine().ToLower();

                if (type == "exit") break;

                Console.Write("Enter amount: $ ");
                double amount = double.Parse(Console.ReadLine());

                if (type == "deposit")
                {
                    balance += amount;
                    transactions.Add($"Deposited: ${amount}");
                }
                else if (type == "withdraw")
                {
                    if (amount <= balance)
                    {
                        balance -= amount;
                        transactions.Add($"Withdrew: ${amount}");
                    }
                    else
                        Console.WriteLine("Insufficient balance.");
                }
                else
                    Console.WriteLine("Invalid transaction type.");
            }

            Console.WriteLine("\nTransaction History:");
            foreach (var t in transactions)
            {
                Console.WriteLine(t);
            }
            Console.WriteLine($"Final Balance: ${balance}");
        }
        public void OOPTask()
        {
            Customer customer = new Customer(1, "Alex", "Miller", "alexmiller@mail.com", "9876543210", "LA");
            Account account = new Account("1011", "Savings", 5000);

            customer.PrintCustomerInfo();
            account.PrintAccountInfo();

            account.Deposit(1500);
            account.Withdraw(2000);
            account.CalculateInterest();
        }
    }

    public class Customer
    {
        private int customerId;
        private string firstName, lastName, email, phone, address;

        public Customer() { }

        public Customer(int id, string fname, string lname, string email, string phone, string address)
        {
            this.customerId = id;
            this.firstName = fname;
            this.lastName = lname;
            this.email = email;
            this.phone = phone;
            this.address = address;
        }

        public void PrintCustomerInfo()
        {
            Console.WriteLine($"\nCustomer Info:\nID: {customerId}\nName: {firstName} {lastName}\nEmail: {email}\nPhone: {phone}\nAddress: {address}");
        }
    }

    public class Account
    {
        private string accountNumber, accountType;
        private double balance;

        public Account() { }

        public Account(string number, string type, double balance)
        {
            this.accountNumber = number;
            this.accountType = type;
            this.balance = balance;
        }

        public void PrintAccountInfo()
        {
            Console.WriteLine($"\nAccount Info:\nNumber: {accountNumber}\nType: {accountType}\nBalance: ${balance}");
        }

        public void Deposit(double amount)
        {
            balance += amount;
            Console.WriteLine($"Deposited ${amount}. New balance: ${balance}");
        }

        public void Withdraw(double amount)
        {
            if (amount <= balance)
            {
                balance -= amount;
                Console.WriteLine($"Withdrew ${amount}. New balance: ${balance}");
            }
            else
                Console.WriteLine("Insufficient balance.");
        }

        public void CalculateInterest()
        {
            double interestRate = 4.5;
            double interest = balance * interestRate / 100;
            balance += interest;
            Console.WriteLine($"Interest added: ${interest:F2}. New balance: ${balance:F2}");
        }
    }
}
