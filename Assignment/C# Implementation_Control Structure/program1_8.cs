namespace Banking_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BankTasks bank = new BankTasks();
            task8_10 banks= new task8_10();
            

            Console.WriteLine("Select a Task to Run:");
            Console.WriteLine("1. Loan Eligibility");
            Console.WriteLine("2. ATM Transaction");
            Console.WriteLine("3. Compound Interest");
            Console.WriteLine("4. Account Balance Check");
            Console.WriteLine("5. Password Validation");
            Console.WriteLine("6. Transaction History");
            Console.WriteLine("7. Customer & Account OOP Task");
            Console.WriteLine("8. Inheritance and Polymorphism");


            Console.Write("Enter task number: ");
            int task = int.Parse(Console.ReadLine());

            switch (task)
            {
                case 1:
                    bank.LoanEligibility();
                    break;
                case 2:
                    bank.ATMTransaction();
                    break;
                case 3:
                    bank.CompoundInterest();
                    break;
                case 4:
                    bank.AccountBalanceCheck();
                    break;
                case 5:
                    bank.PasswordValidation();
                    break;
                case 6:
                    bank.TransactionHistory();
                    break;
                case 7:
                    bank.OOPTask();
                    break;
                case 8:
                    banks.InheritancePolymorphismTask();
                    break;
                default:
                    Console.WriteLine("Invalid task selected.");
                    break;
            }
        }
    }
}
