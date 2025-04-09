/*using System;
using System.Collections.Generic;

namespace CareerHub
{
    class ExceptionDemo
    {

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Exception Handling Demo:");
                Console.WriteLine("1. Validate Email");
                Console.WriteLine("2. Calculate Average Salary");
                Console.WriteLine("3. Upload Resume File");
                Console.WriteLine("4. Submit Application Before Deadline");
                Console.WriteLine("5. Test Database Connection");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter Email: ");
                        string email = Console.ReadLine();
                        Console.WriteLine();
                        EmailExceptionHandler.ValidateEmail(email);
                        break;

                    case "2":
                        var salaries = new List<decimal> { 35000, -50000, 42000 };
                        SalaryExceptionHandler.CalculateAverageSalary(salaries);
                        break;

                    case "3":
                        Console.Write("Enter Resume Path: ");
                        string path = Console.ReadLine();
                        Console.WriteLine();
                        FileUploadExceptionHandler.UploadResume(path);
                        break;

                    case "4":
                        DateTime deadline = new DateTime(2025, 4, 10);
                        Console.WriteLine($"Deadline is: {deadline}");
                        Console.WriteLine($"Submitting now: {DateTime.Now}");
                        Console.WriteLine();
                        ApplicationDeadlineHandler.SubmitApplication(deadline, DateTime.Now);
                        break;

                    case "5":
                        Console.WriteLine("Fetching jobs from database...\n");
                        new DatabaseExceptionHandler().FetchJobs();
                        break;

                    case "6":
                        Console.WriteLine("Exiting program.");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please select between 1-6.");
                        break;

                }

                Console.WriteLine("\n----------------------------------------\n");

            }
        }
    }
}
*/