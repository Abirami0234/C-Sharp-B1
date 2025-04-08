using System;

namespace Banking_System
{
    internal class Task13_Main
    {
        static void Main(string[] args)
        {
            IBankServiceProvider bank = null;

            Console.WriteLine("Select Collection Type:");
            Console.WriteLine("1. List");
            Console.WriteLine("2. Set (HashSet with sorting)");
            Console.WriteLine("3. Dictionary (HashMap)");
            Console.Write("Enter your choice: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    bank = new ListBasedBank();
                    break;
                case "2":
                    bank = new SetBasedBank();
                    break;
                case "3":
                    bank = new MapBasedBank();
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    return;
            }

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n--- Task 13 Menu ---");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. List Accounts");
                Console.WriteLine("3. Calculate Interest");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");
                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        try
                        {
                            Console.Write("Enter Account Number: ");
                            long accNo = long.Parse(Console.ReadLine()!);

                            Console.Write("Enter Account Type (Savings / Current / ZeroBalance): ");
                            string accType = Console.ReadLine()!;

                            Console.Write("Enter Initial Balance: ");
                            float balance = float.Parse(Console.ReadLine()!);

                            Console.Write("Customer ID: ");
                            long custId = long.Parse(Console.ReadLine()!);

                            Console.Write("First Name: ");
                            string firstName = Console.ReadLine()!;

                            Console.Write("Last Name: ");
                            string lastName = Console.ReadLine()!;

                            Console.Write("Email: ");
                            string email = Console.ReadLine()!;

                            Console.Write("Phone: ");
                            string phone = Console.ReadLine()!;

                            Console.Write("Address: ");
                            string address = Console.ReadLine()!;

                            task10_Customer customer = new task10_Customer(custId, firstName, lastName, email, phone, address);
                            bank.create_account(accNo, accType, balance, customer);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;

                    case "2":
                        var accs = bank.listAccounts();
                        Console.WriteLine("\n--- Account List ---");
                        foreach (var acc in accs)
                        {
                            Console.WriteLine($"Account No: {acc.AccountNumber}, Type: {acc.AccountType}, Balance: ₹{acc.Balance}, Customer: {acc.Customer.FirstName} {acc.Customer.LastName}");
                        }
                        break;

                    case "3":
                        bank.calculateInterest();
                        break;

                    case "4":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }

            Console.WriteLine("Exited Task 13 Banking System.");
        }
    }
}
