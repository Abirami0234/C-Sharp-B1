using System;
using System.Data.SqlClient;

namespace CareerHub
{
    public class JobApplicationSubmission
    {
        public static void SubmitNewJobApplication()
        {
            Console.WriteLine("\n[Job Application Submission]");

            Console.Write("Enter applicant name: ");
            string applicantName = Console.ReadLine();

            Console.Write("Enter email: ");
            string email = Console.ReadLine();

            Console.Write("Enter job ID you are applying for: ");
            int jobId = int.Parse(Console.ReadLine());

            Console.Write("Enter cover letter: ");
            string coverLetter = Console.ReadLine();

            string connectionString = "Server=ABIRAMI;Database=CarrerHub;Trusted_Connection=True;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"INSERT INTO Application (ApplicantName, Email, JobID, CoverLetter)
                                     VALUES (@ApplicantName, @Email, @JobID, @CoverLetter)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ApplicantName", applicantName);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@JobID", jobId);
                    command.Parameters.AddWithValue("@CoverLetter", coverLetter);

                    int rowsInserted = command.ExecuteNonQuery();

                    Console.WriteLine(rowsInserted > 0
                        ? "✅ Application submitted successfully!"
                        : "⚠️ Failed to submit application.");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"❌ Error while submitting application: {ex.Message}");
            }
        }
    }
}
