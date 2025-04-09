using System;
using System.Data.SqlClient;

namespace CareerHub
{
    public class SalaryRangeQuery
    {
        public static void RunSalaryRangeQuery()
        {
            Console.WriteLine("\n[Search Jobs by Salary Range]");

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

                    string query = @"SELECT JobID, CompanyName, JobTitle, JobLocation, MinSalary, MaxSalary
                                     FROM Jobs
                                     WHERE MinSalary >= @MinSalary AND MaxSalary <= @MaxSalary";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MinSalary", minSalary);
                    command.Parameters.AddWithValue("@MaxSalary", maxSalary);

                    SqlDataReader reader = command.ExecuteReader();

                    Console.WriteLine("\nMatching Job Listings:");
                    bool found = false;

                    while (reader.Read())
                    {
                        found = true;
                        Console.WriteLine($"Job ID: {reader["JobID"]}");
                        Console.WriteLine($"Company: {reader["CompanyName"]}");
                        Console.WriteLine($"Title: {reader["JobTitle"]}");
                        Console.WriteLine($"Location: {reader["JobLocation"]}");
                        Console.WriteLine($"Salary: {reader["MinSalary"]} - {reader["MaxSalary"]}");
                        Console.WriteLine("-----------------------------------");
                    }

                    if (!found)
                    {
                        Console.WriteLine("No jobs found in the given salary range.");
                    }

                    reader.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error during salary range query: {ex.Message}");
            }
        }
    }
}
