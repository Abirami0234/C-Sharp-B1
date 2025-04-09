using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace CareerHub
{
    public class DatabaseManager
    {
        private string connectionString = "Server=ABIRAMI; Database=CarrerHub; Integrated Security=True;";


        public void InitializeDatabase()
        {
            Console.WriteLine("Database and tables are assumed to be already created.");
        }

        public void InsertCompany(Company company)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            string query = "INSERT INTO companies (companyname, location) VALUES (@name, @location)";
            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@name", company.CompanyName);
            cmd.Parameters.AddWithValue("@location", company.Location);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void InsertJobListing(JobListing job)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            string query = @"INSERT INTO jobs (companyid, jobtitle, jobdescription, joblocation, salary, jobtype) 
                             VALUES (@cid, @title, @desc, @loc, @sal, @type)";
            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@cid", job.CompanyID);
            cmd.Parameters.AddWithValue("@title", job.JobTitle);
            cmd.Parameters.AddWithValue("@desc", job.JobDescription);
            cmd.Parameters.AddWithValue("@loc", job.JobLocation);
            cmd.Parameters.AddWithValue("@sal", job.Salary);
            cmd.Parameters.AddWithValue("@type", job.JobType);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void InsertApplicant(Applicant applicant)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            string query = @"INSERT INTO applicants (firstname, lastname, email, phone, resume, experience, city, state)
                             VALUES (@fname, @lname, @email, @phone, @resume, @exp, @city, @state)";
            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@fname", applicant.FirstName);
            cmd.Parameters.AddWithValue("@lname", applicant.LastName);
            cmd.Parameters.AddWithValue("@email", applicant.Email);
            cmd.Parameters.AddWithValue("@phone", applicant.Phone);
            cmd.Parameters.AddWithValue("@resume", applicant.Resume);
            cmd.Parameters.AddWithValue("@exp", applicant.Experience);
            cmd.Parameters.AddWithValue("@city", applicant.City);
            cmd.Parameters.AddWithValue("@state", applicant.State);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void InsertJobApplication(JobApplication application)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            string query = @"INSERT INTO applications (jobid, applicantid, coverletter) 
                             VALUES (@jid, @aid, @cover)";
            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@jid", application.JobID);
            cmd.Parameters.AddWithValue("@aid", application.ApplicantID);
            cmd.Parameters.AddWithValue("@cover", application.CoverLetter);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public List<JobListing> GetJobListings()
        {
            List<JobListing> list = new();
            using SqlConnection conn = new SqlConnection(connectionString);
            string query = "SELECT * FROM jobs";
            using SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new JobListing
                {
                    JobID = (int)reader["jobid"],
                    CompanyID = (int)reader["companyid"],
                    JobTitle = reader["jobtitle"].ToString(),
                    JobDescription = reader["jobdescription"].ToString(),
                    JobLocation = reader["joblocation"].ToString(),
                    Salary = Convert.ToDecimal(reader["salary"]),
                    JobType = reader["jobtype"].ToString(),
                    PostedDate = Convert.ToDateTime(reader["posteddate"])
                });
            }
            return list;
        }

        public List<Company> GetCompanies()
        {
            List<Company> list = new();
            using SqlConnection conn = new SqlConnection(connectionString);
            string query = "SELECT * FROM companies";
            using SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Company
                {
                    CompanyID = (int)reader["companyid"],
                    CompanyName = reader["companyname"].ToString(),
                    Location = reader["location"].ToString()
                });
            }
            return list;
        }

        public List<Applicant> GetApplicants()
        {
            List<Applicant> list = new();
            using SqlConnection conn = new SqlConnection(connectionString);
            string query = "SELECT * FROM applicants";
            using SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Applicant
                {
                    ApplicantID = (int)reader["applicantid"],
                    FirstName = reader["firstname"].ToString(),
                    LastName = reader["lastname"].ToString(),
                    Email = reader["email"].ToString(),
                    Phone = reader["phone"].ToString(),
                    Resume = reader["resume"].ToString(),
                    Experience = (int)reader["experience"],
                    City = reader["city"].ToString(),
                    State = reader["state"].ToString()
                });
            }
            return list;
        }

        public List<JobApplication> GetApplicationsForJob(int jobID)
        {
            List<JobApplication> list = new();
            using SqlConnection conn = new SqlConnection(connectionString);
            string query = "SELECT * FROM applications WHERE jobid = @jobid";
            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@jobid", jobID);
            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new JobApplication
                {
                    ApplicationID = (int)reader["applicationid"],
                    JobID = (int)reader["jobid"],
                    ApplicantID = (int)reader["applicantid"],
                    ApplicationDate = Convert.ToDateTime(reader["applicationdate"]),
                    CoverLetter = reader["coverletter"].ToString()
                });
            }
            return list;
        }
    }

}
