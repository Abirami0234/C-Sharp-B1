using System;

namespace Banking_System
{
    internal class BankApp
    {
        static void Main(string[] args)
        {
            Bank bank = new Bank();

            while (true) 
            { 

            Console.WriteLine("\n======= Banking System Menu =======");
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdraw");
            Console.WriteLine("4. Get Balance");
            Console.WriteLine("5. Transfer");
            Console.WriteLine("6. Get Account Details");
            Console.WriteLine("7. Exit");
            Console.Write("Enter your choice (1-7): ");
            string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        // Create Account
                        Console.Write("Enter Customer ID: ");
                        long custId = long.Parse(Console.ReadLine());
                        Console.Write("Enter First Name: ");
                        string? firstName = Console.ReadLine();
                        Console.Write("Enter Last Name: ");
                        string? lastName = Console.ReadLine();
                        Console.Write("Enter Email: ");
                        string? email = Console.ReadLine();
                        Console.Write("Enter Phone: ");
                        string? phone = Console.ReadLine();
                        Console.Write("Enter Address: ");
                        string? address = Console.ReadLine();
                        Console.WriteLine("Choose Account Type:");
                        Console.WriteLine("1. Savings");
                        Console.WriteLine("2. Current");
                        Console.Write("Enter option: ");
                        string? accTypeOption = Console.ReadLine();
                        string accType = accTypeOption == "1" ? "Savings" : "Current";
                        Console.Write("Enter initial deposit: ");
                        float balance = float.Parse(Console.ReadLine());

                        task10_Customer cust = new task10_Customer(custId, firstName, lastName, email, phone, address);
                        long accNo = bank.CreateAccount(cust, accType, balance);
                        Console.WriteLine($"Account created successfully! Account Number: {accNo}");
                        break;

                    case "2":
                        // Deposit
                        try
                        {
                            Console.Write("Enter Account Number: ");
                            long depAcc = long.Parse(Console.ReadLine());

                            Console.Write("Enter amount to deposit: ");
                            float depAmt = float.Parse(Console.ReadLine());

                            float newBal = bank.Deposit(depAcc, depAmt);
                            Console.WriteLine($"Deposit successful. New Balance: {newBal}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;

                    case "3":
                        // Withdraw
                        try
                        {
                            Console.Write("Enter Account Number: ");
                            long wAcc = long.Parse(Console.ReadLine());
                            Console.Write("Enter amount to withdraw: ");
                            float wAmt = float.Parse(Console.ReadLine());
                            float wBal = bank.Withdraw(wAcc, wAmt);
                            Console.WriteLine($"New Balance: {wBal}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;

                    case "4":
                        // Get Balance
                        try
                        {
                            Console.Write("Enter Account Number: ");
                            long bAcc = long.Parse(Console.ReadLine());
                            float bal = bank.GetAccountBalance(bAcc);
                            Console.WriteLine($"Current Balance: {bal}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;

                    case "5":
                        // Transfer
                        try
                        {
                            Console.Write("Enter From Account Number: ");
                            long fromAcc = long.Parse(Console.ReadLine());
                            Console.Write("Enter To Account Number: ");
                            long toAcc = long.Parse(Console.ReadLine());
                            Console.Write("Enter amount to transfer: ");
                            float tAmt = float.Parse(Console.ReadLine());
                            bank.Transfer(fromAcc, toAcc, tAmt);
                            Console.WriteLine("Transfer successful.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;

                    case "6":
                        // Get Account Details
                        try
                        {
                            Console.Write("Enter Account Number: ");
                            long detAcc = long.Parse(Console.ReadLine());
                            bank.GetAccountDetails(detAcc);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;

                    case "7":
                        Console.WriteLine("Thank you for using the banking system!");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 7.");
                        break;
                }
            }
        }
    }
}
