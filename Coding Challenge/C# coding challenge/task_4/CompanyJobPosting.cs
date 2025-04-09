using System;
using System.Data.SqlClient;

namespace CareerHub
{
    public class CompanyJobPosting
    {
        public static void PostNewCompanyJob()
        {
            Console.WriteLine("\n[Company Job Posting]");

            Console.Write("Enter company name: ");
            string companyName = Console.ReadLine();

            Console.Write("Enter job title: ");
            string jobTitle = Console.ReadLine();

            Console.Write("Enter job description: ");
            string jobDescription = Console.ReadLine();

            Console.Write("Enter job location: ");
            string jobLocation = Console.ReadLine();

            Console.Write("Enter job type (Full-time / Part-time / Remote): ");
            string jobType = Console.ReadLine();

            Console.Write("Enter minimum salary: ");
            decimal minSalary = decimal.Parse(Console.ReadLine());

            Console.Write("Enter maximum salary: ");
            decimal maxSalary = decimal.Parse(Console.ReadLine());

            string connectionString = "Server=ABIRAMI;Database=CarrerHub;Trusted_Connection=True;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"INSERT INTO Jobs 
                                    (CompanyName, JobTitle, JobDescription, JobLocation, JobType, MinSalary, MaxSalary)
                                     VALUES 
                                    (@CompanyName, @JobTitle, @JobDescription, @JobLocation, @JobType, @MinSalary, @MaxSalary)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CompanyName", companyName);
                    command.Parameters.AddWithValue("@JobTitle", jobTitle);
                    command.Parameters.AddWithValue("@JobDescription", jobDescription);
                    command.Parameters.AddWithValue("@JobLocation", jobLocation);
                    command.Parameters.AddWithValue("@JobType", jobType);
                    command.Parameters.AddWithValue("@MinSalary", minSalary);
                    command.Parameters.AddWithValue("@MaxSalary", maxSalary);

                    int rowsInserted = command.ExecuteNonQuery();

                    Console.WriteLine(rowsInserted > 0
                        ? "✅ Job posted successfully!"
                        : "⚠️ Failed to post job.");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"❌ Error while posting job: {ex.Message}");
            }
        }
    }
}
