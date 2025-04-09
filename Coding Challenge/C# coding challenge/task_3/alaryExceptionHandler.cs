using System;
using System.Collections.Generic;

namespace CareerHub
{
    public class SalaryExceptionHandler
    {
        public static void CalculateAverageSalary(List<decimal> salaries)
        {
            try
            {
                if (salaries.Count == 0) throw new Exception("No salaries provided.");

                decimal total = 0;
                foreach (var salary in salaries)
                {
                    if (salary < 0)
                        throw new ArgumentException($"Negative salary found: {salary}");

                    total += salary;
                }

                decimal average = total / salaries.Count;
                Console.WriteLine($"Average Salary: {average:C}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Salary Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
            }
        }
    }
}
