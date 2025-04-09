using System;
using System.Collections.Generic;

namespace CareerHub
{
    public class JobListing
    {
        public int JobID { get; set; }
        public int CompanyID { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string JobLocation { get; set; }
        public decimal Salary { get; set; }
        public string JobType { get; set; }
        public DateTime PostedDate { get; set; }

        private List<JobApplication> applications = new List<JobApplication>();

        public void Apply(int applicantID, string coverLetter)
        {
            var application = new JobApplication
            {
                ApplicationID = new Random().Next(10000, 99999),
                JobID = this.JobID,
                ApplicantID = applicantID,
                CoverLetter = coverLetter,
                ApplicationDate = DateTime.Now
            };
            applications.Add(application);
            Console.WriteLine($"Applicant {applicantID} applied for job '{JobTitle}'");
        }

        public List<JobApplication> GetApplicants()
        {
            return applications;
        }
    }
}
