using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Banking_System
{
    public class BankRepositoryImpl : IBankRepository
    {
        public void createAccount(task10_Customer customer, long accNo, string accType, float balance)
        {
            using SqlConnection conn = DBUtil.getDBConn();
            conn.Open();

            SqlTransaction transaction = conn.BeginTransaction();

            try
            {
                // Insert Customer
                SqlCommand custCmd = new SqlCommand("INSERT INTO Customer VALUES (@Id, @FName, @LName, @Email, @Phone, @Address)", conn, transaction);
                custCmd.Parameters.AddWithValue("@Id", customer.CustomerID);
                custCmd.Parameters.AddWithValue("@FName", customer.FirstName);
                custCmd.Parameters.AddWithValue("@LName", customer.LastName);
                custCmd.Parameters.AddWithValue("@Email", customer.Email);
                custCmd.Parameters.AddWithValue("@Phone", customer.Phone);
                custCmd.Parameters.AddWithValue("@Address", customer.Address);
                custCmd.ExecuteNonQuery();

                // Insert Account
                SqlCommand accCmd = new SqlCommand("INSERT INTO Account VALUES (@AccNo, @AccType, @Balance, @CustId)", conn, transaction);
                accCmd.Parameters.AddWithValue("@AccNo", accNo);
                accCmd.Parameters.AddWithValue("@AccType", accType);
                accCmd.Parameters.AddWithValue("@Balance", balance);
                accCmd.Parameters.AddWithValue("@CustId", customer.CustomerID);
                accCmd.ExecuteNonQuery();

                transaction.Commit();
                Console.WriteLine("Account and Customer created successfully.");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public List<task10_Account> listAccounts()
        {
            List<task10_Account> accounts = new List<task10_Account>();
            using SqlConnection conn = DBUtil.getDBConn();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Account JOIN Customer ON Account.CustomerId = Customer.CustomerId", conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var customer = new task10_Customer(
                    (long)reader["CustomerId"],
                    reader["FirstName"].ToString(),
                    reader["LastName"].ToString(),
                    reader["Email"].ToString(),
                    reader["Phone"].ToString(),
                    reader["Address"].ToString()
                );

                var acc = new task10_Account(
                    (long)reader["AccountNumber"],
                    reader["AccountType"].ToString(),
                    float.Parse(reader["AccountBalance"].ToString()),
                    customer
                );

                accounts.Add(acc);
            }

            return accounts;
        }

        public float getAccountBalance(long accountNumber)
        {
            using SqlConnection conn = DBUtil.getDBConn();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT AccountBalance FROM Account WHERE AccountNumber = @AccNo", conn);
            cmd.Parameters.AddWithValue("@AccNo", accountNumber);

            var result = cmd.ExecuteScalar();
            if (result != null)
                return Convert.ToSingle(result);

            throw new InvalidAccountException("Account not found.");
        }

        public float deposit(long accountNumber, float amount)
        {
            using SqlConnection conn = DBUtil.getDBConn();
            conn.Open();

            float currentBalance = getAccountBalance(accountNumber);
            float newBalance = currentBalance + amount;

            SqlCommand cmd = new SqlCommand("UPDATE Account SET AccountBalance = @NewBal WHERE AccountNumber = @AccNo", conn);
            cmd.Parameters.AddWithValue("@NewBal", newBalance);
            cmd.Parameters.AddWithValue("@AccNo", accountNumber);
            cmd.ExecuteNonQuery();

            InsertTransaction(accountNumber, "Deposit", "Amount deposited", amount);
            return newBalance;
        }

        public float withdraw(long accountNumber, float amount)
        {
            using SqlConnection conn = DBUtil.getDBConn();
            conn.Open();

            float currentBalance = getAccountBalance(accountNumber);
            if (currentBalance < amount)
                throw new InsufficientFundException("Insufficient funds.");

            float newBalance = currentBalance - amount;

            SqlCommand cmd = new SqlCommand("UPDATE Account SET AccountBalance = @NewBal WHERE AccountNumber = @AccNo", conn);
            cmd.Parameters.AddWithValue("@NewBal", newBalance);
            cmd.Parameters.AddWithValue("@AccNo", accountNumber);
            cmd.ExecuteNonQuery();

            InsertTransaction(accountNumber, "Withdraw", "Amount withdrawn", amount);
            return newBalance;
        }

        public void transfer(long fromAccountNumber, long toAccountNumber, float amount)
        {
            using SqlConnection conn = DBUtil.getDBConn();
            conn.Open();

            SqlTransaction transaction = conn.BeginTransaction();

            try
            {
                float fromBalance = getAccountBalance(fromAccountNumber);
                if (fromBalance < amount)
                    throw new InsufficientFundException("Insufficient funds to transfer.");

                float toBalance = getAccountBalance(toAccountNumber);

                // Withdraw from sender
                SqlCommand withdrawCmd = new SqlCommand("UPDATE Account SET AccountBalance = @NewBal WHERE AccountNumber = @AccNo", conn, transaction);
                withdrawCmd.Parameters.AddWithValue("@NewBal", fromBalance - amount);
                withdrawCmd.Parameters.AddWithValue("@AccNo", fromAccountNumber);
                withdrawCmd.ExecuteNonQuery();

                // Deposit to receiver
                SqlCommand depositCmd = new SqlCommand("UPDATE Account SET AccountBalance = @NewBal WHERE AccountNumber = @AccNo", conn, transaction);
                depositCmd.Parameters.AddWithValue("@NewBal", toBalance + amount);
                depositCmd.Parameters.AddWithValue("@AccNo", toAccountNumber);
                depositCmd.ExecuteNonQuery();

                // Log transactions
                InsertTransaction(fromAccountNumber, "Transfer", $"Transferred to {toAccountNumber}", amount, conn, transaction);
                InsertTransaction(toAccountNumber, "Transfer", $"Received from {fromAccountNumber}", amount, conn, transaction);

                transaction.Commit();
                Console.WriteLine("Transfer successful.");
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public task10_Account getAccountDetails(long accountNumber)
        {
            using SqlConnection conn = DBUtil.getDBConn();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Account JOIN Customer ON Account.CustomerId = Customer.CustomerId WHERE Account.AccountNumber = @AccNo", conn);
            cmd.Parameters.AddWithValue("@AccNo", accountNumber);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                var customer = new task10_Customer(
                    (long)reader["CustomerId"],
                    reader["FirstName"].ToString(),
                    reader["LastName"].ToString(),
                    reader["Email"].ToString(),
                    reader["Phone"].ToString(),
                    reader["Address"].ToString()
                );

                return new task10_Account(
                    (long)reader["AccountNumber"],
                    reader["AccountType"].ToString(),
                    float.Parse(reader["AccountBalance"].ToString()),
                    customer
                );
            }

            throw new InvalidAccountException("Account not found.");
        }

        public List<Transaction> getTransactions(long accountNumber, DateTime fromDate, DateTime toDate)
        {
            List<Transaction> transactions = new();

            using SqlConnection conn = DBUtil.getDBConn();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Transaction WHERE AccountNumber = @AccNo AND TransactionDate BETWEEN @FromDate AND @ToDate", conn);
            cmd.Parameters.AddWithValue("@AccNo", accountNumber);
            cmd.Parameters.AddWithValue("@FromDate", fromDate);
            cmd.Parameters.AddWithValue("@ToDate", toDate);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                transactions.Add(new Transaction
                {
                    AccountNumber = (long)reader["AccountNumber"],
                    TransactionType = reader["TransactionType"].ToString(),
                    Description = reader["Description"].ToString(),
                    TransactionAmount = float.Parse(reader["TransactionAmount"].ToString()),
                    TransactionDate = DateTime.Parse(reader["TransactionDate"].ToString())
                });
            }

            return transactions;
        }

        private void InsertTransaction(long accNo, string type, string description, float amount, SqlConnection? conn = null, SqlTransaction? trans = null)
        {
            bool externalConnection = conn != null;

            if (!externalConnection)
                conn = DBUtil.getDBConn();

            using SqlCommand cmd = new SqlCommand("INSERT INTO Transaction VALUES (@AccNo, @Desc, @Date, @Type, @Amount)", conn, trans);
            cmd.Parameters.AddWithValue("@AccNo", accNo);
            cmd.Parameters.AddWithValue("@Desc", description);
            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@Amount", amount);

            cmd.ExecuteNonQuery();
        }

        public void calculateInterest()
        {
            using SqlConnection conn = DBUtil.getDBConn();
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE Account SET AccountBalance = AccountBalance + (AccountBalance * 0.045) WHERE AccountType = 'Savings'", conn);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Interest calculated and added for savings accounts.");
        }
    }
}
