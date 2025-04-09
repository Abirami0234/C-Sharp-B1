using System;
using System.Collections.Generic;

namespace CareerHub
{
    public class Company
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }

        public List<JobListing> JobListings { get; private set; } = new List<JobListing>();

        public void PostJob(string jobTitle, string jobDescription, string jobLocation, decimal salary, string jobType)
        {
            var newJob = new JobListing
            {
                JobID = new Random().Next(1000, 9999),
                CompanyID = this.CompanyID,
                JobTitle = jobTitle,
                JobDescription = jobDescription,
                JobLocation = jobLocation,
                Salary = salary,
                JobType = jobType,
                PostedDate = DateTime.Now
            };

            JobListings.Add(newJob);
            Console.WriteLine($"Job '{jobTitle}' posted by {CompanyName}.");
        }

        public List<JobListing> GetJobs()
        {
            return JobListings;
        }
    }
}
