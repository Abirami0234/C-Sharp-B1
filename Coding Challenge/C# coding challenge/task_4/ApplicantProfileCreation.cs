using System;
using System.Data.SqlClient;

namespace CareerHub
{
    public class ApplicantProfileCreation
    {
        public static void CreateNewApplicantProfile()
        {
            Console.WriteLine("\n[Applicant Profile Creation]");

            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            Console.Write("Enter Phone: ");
            string phone = Console.ReadLine();

            Console.Write("Enter Resume File Name (or path): ");
            string resume = Console.ReadLine();

            Console.Write("Enter Experience (in years): ");
            int experience = int.Parse(Console.ReadLine());

            Console.Write("Enter City: ");
            string city = Console.ReadLine();

            Console.Write("Enter State: ");
            string state = Console.ReadLine();

            string connectionString = "Server=ABIRAMI;Database=CarrerHub;Trusted_Connection=True;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"INSERT INTO Applicants 
                                    (FirstName, LastName, Email, Phone, Resume, Experience, City, State)
                                     VALUES 
                                    (@FirstName, @LastName, @Email, @Phone, @Resume, @Experience, @City, @State)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@Resume", resume);
                    command.Parameters.AddWithValue("@Experience", experience);
                    command.Parameters.AddWithValue("@City", city);
                    command.Parameters.AddWithValue("@State", state);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                        Console.WriteLine(" Applicant profile created successfully.");
                    else
                        Console.WriteLine(" No record inserted. Please try again.");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"An error occurred while creating the applicant profile: {ex.Message}");
            }
        }
    }
}

