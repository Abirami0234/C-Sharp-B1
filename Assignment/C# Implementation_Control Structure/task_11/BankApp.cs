using System;

namespace Banking_System
{
    public class BankApp
    {
        public static void Main(string[] args)
        {
            BankServiceProviderImpl bankService = new BankServiceProviderImpl("Hexa Bank", "Dindigul Branch");

            while (true)
            {
                Console.WriteLine("\n--- Welcome to Hexa Bank ---");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Get Balance");
                Console.WriteLine("5. Transfer");
                Console.WriteLine("6. Get Account Details");
                Console.WriteLine("7. List Accounts");
                Console.WriteLine("8. Calculate Interest");
                Console.WriteLine("9. Exit");

                Console.Write("Enter your choice (1-9): ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        CreateAccount(bankService);
                        break;
                    case "2":
                        Deposit(bankService);
                        break;
                    case "3":
                        Withdraw(bankService);
                        break;
                    case "4":
                        GetBalance(bankService);
                        break;
                    case "5":
                        Transfer(bankService);
                        break;
                    case "6":
                        GetAccountDetails(bankService);
                        break;
                    case "7":
                        ListAccounts(bankService);
                        break;
                    case "8":
                        bankService.calculateInterest();
                        break;
                    case "9":
                        Console.WriteLine("Thank you for banking with us!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }

        static void CreateAccount(BankServiceProviderImpl bankService)
        {
            Console.Write("Enter Customer ID: ");
            long id = long.Parse(Console.ReadLine());
            Console.Write("Enter First Name: ");
            string? firstName = Console.ReadLine();
            Console.Write("Enter Last Name: ");
            string? lastName = Console.ReadLine();
            Console.Write("Enter Email: ");
            string? email = Console.ReadLine();
            Console.Write("Enter Phone: ");
            string? phone = Console.ReadLine();
                      
            Console.Write("Enter Customer Address: ");
            string address = Console.ReadLine();

            

            Console.WriteLine("\nSelect Account Type:");
            Console.WriteLine("1. Savings Account");
            Console.WriteLine("2. Current Account");
            Console.WriteLine("3. Zero Balance Account");
            Console.Write("Choice: ");
            string choice = Console.ReadLine();

            Console.Write("Enter Initial Balance: ");
            float balance = float.Parse(Console.ReadLine());

            var customer = new task10_Customer(id, firstName, lastName, email, phone ,address); 
            Console.Write("Enter Account Number: ");
            long accNo = long.Parse(Console.ReadLine());
            string accType = "";

            switch (choice)
            {
                case "1":
                    accType = "Savings";
                    break;
                case "2":
                    accType = "Current";
                    break;
                case "3":
                    accType = "ZeroBalance";
                    break;
                default:
                    Console.WriteLine("Invalid account type.");
                    return;
            }

            bankService.create_account(accNo, accType,balance,customer); 
        }

        static void Deposit(BankServiceProviderImpl bankService)
        {
            Console.Write("Enter Account Number: ");
            long accNo = long.Parse(Console.ReadLine());
            Console.Write("Enter Amount to Deposit: ");
            float amount = float.Parse(Console.ReadLine());
            float balance = bankService.deposit(accNo, amount);
            Console.WriteLine($"Amount deposited. New Balance: {balance}");
        }

        static void Withdraw(BankServiceProviderImpl bankService)
        {
            Console.Write("Enter Account Number: ");
            long accNo = long.Parse(Console.ReadLine());
            Console.Write("Enter Amount to Withdraw: ");
            float amount = float.Parse(Console.ReadLine());

            float balance = bankService.withdraw(accNo, amount);
            Console.WriteLine($"Amount withdrawn. New Balance: {balance}");
        }

        static void GetBalance(BankServiceProviderImpl bankService)
        {
            Console.Write("Enter Account Number: ");
            long accNo = long.Parse(Console.ReadLine());
            float balance = bankService.get_account_balance(accNo);
            Console.WriteLine($"Current Balance: {balance}");
        }

        static void Transfer(BankServiceProviderImpl bankService)
        {
            Console.Write("From Account Number: ");
            long fromAcc = long.Parse(Console.ReadLine());
            Console.Write("To Account Number: ");
            long toAcc = long.Parse(Console.ReadLine());
            Console.Write("Amount to Transfer: ");
            float amount = float.Parse(Console.ReadLine());

            bankService.transfer(fromAcc, toAcc, amount);
            Console.WriteLine("Transfer successful.");
        }

        static void GetAccountDetails(BankServiceProviderImpl bankService)
        {
            Console.Write("Enter Account Number: ");
            long accNo = long.Parse(Console.ReadLine());
            bankService.getAccountDetails(accNo);
        }

        static void ListAccounts(BankServiceProviderImpl bankService)
        {
            var accounts = bankService.listAccounts();
            Console.WriteLine("\n--- List of Accounts ---");
            foreach (var acc in accounts)
            {
                acc.PrintAccountInfo();
            }
        }
    }
}
