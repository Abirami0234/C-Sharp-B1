using System;

namespace CareerHub
{
    public class ApplicationDeadlineHandler
    {
        public static void SubmitApplication(DateTime deadline, DateTime submissionTime)
        {
            try
            {
                if (submissionTime > deadline)
                    throw new InvalidOperationException("The application deadline has passed.");

                Console.WriteLine("Application submitted successfully.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Deadline Error: {ex.Message}");
            }
        }
    }
}
