using System;

namespace Banking_System
{
    internal class Task12_Main
    {
        static void Main(string[] args)
        {
            ExceptionHandling bank = new ExceptionHandling();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n--- Task 12: Exception Handling ---");
                Console.WriteLine("1. Withdraw");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Transfer");
                Console.WriteLine("4. Get Account Details");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");

                string? input = Console.ReadLine();
                Console.WriteLine();

                try
                {
                    switch (input)
                    {
                        case "1":
                            Console.Write("Enter account number: ");
                            long wAcc = long.Parse(Console.ReadLine()!);

                            Console.Write("Enter amount to withdraw: ");
                            float wAmt = float.Parse(Console.ReadLine()!);

                            float newBal = bank.Withdraw(wAcc, wAmt);
                            Console.WriteLine($"Withdraw successful. New balance: ₹{newBal}");
                            break;

                        case "2":
                            Console.Write("Enter account number: ");
                            long dAcc = long.Parse(Console.ReadLine()!);

                            Console.Write("Enter amount to deposit: ");
                            float dAmt = float.Parse(Console.ReadLine()!);

                            float updatedBal = bank.Deposit(dAcc, dAmt);
                            Console.WriteLine($"Deposit successful. New balance: ₹{updatedBal}");
                            break;

                        case "3":
                            Console.Write("Enter FROM account number: ");
                            long fromAcc = long.Parse(Console.ReadLine()!);

                            Console.Write("Enter TO account number: ");
                            long toAcc = long.Parse(Console.ReadLine()!);

                            Console.Write("Enter amount to transfer: ");
                            float tAmt = float.Parse(Console.ReadLine()!);

                            bank.Transfer(fromAcc, toAcc, tAmt);
                            break;

                        case "4":
                            Console.Write("Enter account number: ");
                            long detailsAcc = long.Parse(Console.ReadLine()!);

                            bank.GetAccountDetails(detailsAcc);
                            break;

                        case "5":
                            running = false;
                            break;

                        default:
                            Console.WriteLine("Invalid option. Try again.");
                            break;
                    }
                }
                catch (InvalidAccountException ex)
                {
                    Console.WriteLine($"Invalid Account: {ex.Message}");
                }
                catch (InsufficientFundException ex)
                {
                    Console.WriteLine($" Insufficient Funds: {ex.Message}");
                }
                catch (OverDraftLimitExceededException ex)
                {
                    Console.WriteLine($" Overdraft Denied: {ex.Message}");
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine(" Error: Some value was null. Please try again.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($" Unexpected Error: {ex.Message}");
                }
            }

            Console.WriteLine("Exited the Task 12 Banking System.");
        }
    }
}
