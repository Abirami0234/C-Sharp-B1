using System;

namespace CareerHub
{
    public class Applicant
    {
        public int ApplicantID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Resume { get; set; }
        public int Experience { get; set; }    
        public string City { get; set; }
        public string State { get; set; }

        public void CreateProfile(string email, string firstName, string lastName, string phone)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Console.WriteLine("Applicant profile created successfully.");
        }

        public void ApplyForJob(JobListing job, string coverLetter)
        {
            job.Apply(this.ApplicantID, coverLetter);
        }
    }
}
