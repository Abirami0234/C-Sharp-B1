using System;
using Microsoft.Data.SqlClient;

namespace CarConnect
{
    public class DatabaseContext
    {
        private readonly string connectionString = "Server=ABIRAMI;Database=CarConnect;Trusted_Connection=True;";

        public SqlConnection GetConnection()
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                return connection;
            }
            catch (Exception)
            {
                throw new DatabaseConnectionException("Unable to connect to the database.");
            }
        }
    }
}