
using Microsoft.Data.SqlClient;

namespace Banking_System
{
    public static class DBUtil
    {
        private static readonly string connectionString = @"Data Source=ABIRAMI;Initial Catalog=HMBank;Integrated Security=True";

        public static SqlConnection getDBConn()
        {
            return new SqlConnection(connectionString);
        }
    }
}
