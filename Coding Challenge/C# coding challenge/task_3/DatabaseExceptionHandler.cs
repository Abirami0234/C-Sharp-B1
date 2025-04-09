using System;
using System.Data.SqlClient;

namespace CareerHub
{
    public class DatabaseExceptionHandler
    {
        private string connectionString = "Server=ABIRAMI; Database=CarrerHub; Integrated Security=True;";

        public void FetchJobs()
        {
            try
            {
                using SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                string query = "SELECT * FROM jobs";
                using SqlCommand cmd = new SqlCommand(query, conn);
                using SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"Job Title: {reader["jobtitle"]}");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
            }
        }
    }
}
