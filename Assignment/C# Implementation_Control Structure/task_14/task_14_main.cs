using System;
using System.Globalization;

namespace Banking_System
{
    class task_14_main
    {
        static void Main(string[] args)
        {
            IBankServiceProvider bank = new BankServiceProviderImpl("HMBank Branch", "Chennai");

            while (true)
            {
                Console.WriteLine("\n==== HMBank Menu ====");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Transfer");
                Console.WriteLine("5. Get Account Balance");
                Console.WriteLine("6. Get Account Details");
                Console.WriteLine("7. List All Accounts");
                Console.WriteLine("8. Calculate Interest");
                Console.WriteLine("9. Get Transactions (by date)");
                Console.WriteLine("0. Exit");
                Console.Write("Select an option: ");
                string? input = Console.ReadLine();

                try
                {
                    switch (input)
                    {
                        case "1":
                            CreateAccount(bank);
                            break;
                        case "2":
                            Deposit(bank);
                            break;
                        case "3":
                            Withdraw(bank);
                            break;
                        case "4":
                            Transfer(bank);
                            break;
                        case "5":
                            GetBalance(bank);
                            break;
                        case "6":
                            GetDetails(bank);
                            break;
                        case "7":
                            ListAccounts(bank);
                            break;
                        case "8":
                            bank.calculateInterest();
                            break;
                        case "9":
                            GetTransactions(bank);
                            break;
                        case "0":
                            Console.WriteLine("Thank you for using HMBank!");
                            return;
                        default:
                            Console.WriteLine("Invalid option.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Error] {ex.Message}");
                }
            }
        }

        static void CreateAccount(IBankServiceProvider bank)
        {
            Console.Write("Enter Account Number: ");
            long accNo = long.Parse(Console.ReadLine()!);

            Console.Write("Enter Account Type (Savings / Current / ZeroBalance): ");
            string accType = Console.ReadLine()!;

            Console.Write("Enter Initial Balance: ");
            float balance = float.Parse(Console.ReadLine()!);

            Console.Write("Enter Customer ID: ");
            long id = long.Parse(Console.ReadLine()!);

            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine()!;

            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine()!;

            Console.Write("Enter Email: ");
            string email = Console.ReadLine()!;

            Console.Write("Enter Phone: ");
            string phone = Console.ReadLine()!;

            Console.Write("Enter Address: ");
            string address = Console.ReadLine()!;

            task10_Customer customer = new task10_Customer(id, firstName, lastName, email, phone, address);
            bank.create_account(accNo, accType, balance, customer);
        }

        static void Deposit(IBankServiceProvider bank)
        {
            Console.Write("Enter Account Number: ");
            long accNo = long.Parse(Console.ReadLine()!);

            Console.Write("Enter Amount to Deposit: ");
            float amount = float.Parse(Console.ReadLine()!);

            float updated = bank.deposit(accNo, amount);
            Console.WriteLine($"New Balance: ₹{updated}");
        }

        static void Withdraw(IBankServiceProvider bank)
        {
            Console.Write("Enter Account Number: ");
            long accNo = long.Parse(Console.ReadLine()!);

            Console.Write("Enter Amount to Withdraw: ");
            float amount = float.Parse(Console.ReadLine()!);

            float updated = bank.withdraw(accNo, amount);
            Console.WriteLine($"New Balance: ₹{updated}");
        }

        static void Transfer(IBankServiceProvider bank)
        {
            Console.Write("From Account Number: ");
            long from = long.Parse(Console.ReadLine()!);

            Console.Write("To Account Number: ");
            long to = long.Parse(Console.ReadLine()!);

            Console.Write("Enter Amount to Transfer: ");
            float amt = float.Parse(Console.ReadLine()!);

            bank.transfer(from, to, amt);
        }

        static void GetBalance(IBankServiceProvider bank)
        {
            Console.Write("Enter Account Number: ");
            long accNo = long.Parse(Console.ReadLine()!);

            float bal = bank.get_account_balance(accNo);
            Console.WriteLine($"Current Balance: ₹{bal}");
        }

        static void GetDetails(IBankServiceProvider bank)
        {
            Console.Write("Enter Account Number: ");
            long accNo = long.Parse(Console.ReadLine()!);

            var acc = bank.getAccountDetails(accNo);
            acc.PrintAccountInfo();
        }

        static void ListAccounts(IBankServiceProvider bank)
        {
            var accounts = bank.listAccounts();
            foreach (var acc in accounts)
            {
                acc.PrintAccountInfo();
            }
        }

        static void GetTransactions(IBankServiceProvider bank)
        {
            Console.Write("Enter Account Number: ");
            long accNo = long.Parse(Console.ReadLine()!);

            Console.Write("From Date (yyyy-MM-dd): ");
            DateTime from = DateTime.ParseExact(Console.ReadLine()!, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            Console.Write("To Date (yyyy-MM-dd): ");
            DateTime to = DateTime.ParseExact(Console.ReadLine()!, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var txns = bank.getTransations(accNo, from, to);
            foreach (var txn in txns)
            {
                txn.PrintTransaction();
            }
        }
    }
}
