using System;
using System.Text.RegularExpressions;

namespace CareerHub
{
    public class EmailExceptionHandler
    {
        public static void ValidateEmail(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    throw new FormatException("Invalid email format.");
                }

                Console.WriteLine("Email is valid. Proceeding with registration...");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Email Validation Error: {ex.Message}");
            }
        }
    }
}
