/*using System;
using System.Collections.Generic;
using CareerHub;

class Program
{
    static void Main(string[] args)
    {
        
        Company hexaware = new Company
        {
            CompanyID = 1,
            CompanyName = "Google",
            Location = "California"
        };

        hexaware.PostJob(
            "Software Engineer",
            "Develop and maintain software solutions.",
            "California",
            750000,
            "Full-time"
        );

        
        Applicant abirami = new Applicant
        {
            ApplicantID = 101
        };
        abirami.CreateProfile("alexmiller@gmail.com", "Alex", "Miller", "9876543210");
        abirami.Resume = "alex_miller_resume.pdf";

        
        JobListing job1 = hexaware.GetJobs()[0];
        abirami.ApplyForJob(job1, "I’m excited about this role.");

        
        Console.WriteLine("\nApplicants for job: " + job1.JobTitle);
        foreach (var app in job1.GetApplicants())
        {
            Console.WriteLine($" - ApplicantID: {app.ApplicantID}, CoverLetter: {app.CoverLetter}");
        }

        Console.WriteLine("\n---------------------------------------------\n");

        Company infosys = new Company
        {
            CompanyID = 2,
            CompanyName = "Microsoft",
            Location = "Washington"
        };

        
        infosys.PostJob(
            "Data Analyst",
            "Analyze data for insights.",
            "Washington",
            600000,
            "Full-Time"
        );

        Applicant john = new Applicant
        {
            ApplicantID = 202
        };
        john.CreateProfile("emmabrown@gmail.com", "Emma", "Brown", "9123456780");
        john.Resume = "emma_brown_resume.pdf";

        
        JobListing job2 = infosys.GetJobs()[0];
        john.ApplyForJob(job2, "Looking forward to working on data at Microsoft.");

      
        Console.WriteLine("\nApplicants for job: " + job2.JobTitle);
        foreach (var app in job2.GetApplicants())
        {
            Console.WriteLine($" - ApplicantID: {app.ApplicantID}, CoverLetter: {app.CoverLetter}");
        }
    }
}




using System;
using System.Collections.Generic;

namespace CareerHub
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseManager db = new DatabaseManager();
            while (true)
            {
                Console.WriteLine("\n===== CareerHub Menu =====");
                Console.WriteLine("1. View Job Listings");
                Console.WriteLine("2. View Companies");
                Console.WriteLine("3. View Applicants");
                Console.WriteLine("4. View Applications for a Job");
                Console.WriteLine("0. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        List<JobListing> jobs = db.GetJobListings();
                        Console.WriteLine("\n--- Job Listings ---");
                        foreach (var job in jobs)
                        {
                            Console.WriteLine($"ID: {job.JobID}, Title: {job.JobTitle}, CompanyID: {job.CompanyID}, Type: {job.JobType}, Location: {job.JobLocation}, Salary: {job.Salary}, Posted: {job.PostedDate.ToShortDateString()}");
                        }
                        break;

                    case "2":
                        List<Company> companies = db.GetCompanies();
                        Console.WriteLine("\n--- Companies ---");
                        foreach (var company in companies)
                        {
                            Console.WriteLine($"ID: {company.CompanyID}, Name: {company.CompanyName}, Location: {company.Location}");
                        }
                        break;

                    case "3":
                        List<Applicant> applicants = db.GetApplicants();
                        Console.WriteLine("\n--- Applicants ---");
                        foreach (var applicant in applicants)
                        {
                            Console.WriteLine($"ID: {applicant.ApplicantID}, Name: {applicant.FirstName} {applicant.LastName}, Email: {applicant.Email}, Phone: {applicant.Phone}, Experience: {applicant.Experience} yrs, City: {applicant.City}, State: {applicant.State}");
                        }
                        break;

                    case "4":
                        Console.Write("Enter Job ID to view applications: ");
                        if (int.TryParse(Console.ReadLine(), out int jobId))
                        {
                            List<JobApplication> apps = db.GetApplicationsForJob(jobId);
                            Console.WriteLine($"\n--- Applications for Job ID {jobId} ---");
                            foreach (var app in apps)
                            {
                                Console.WriteLine($"ApplicationID: {app.ApplicationID}, ApplicantID: {app.ApplicantID}, CoverLetter: {app.CoverLetter}, Date: {app.ApplicationDate.ToShortDateString()}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Job ID.");
                        }
                        break;

                    case "0":
                        Console.WriteLine("Exiting CareerHub. Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please select from the menu.");
                        break;
                }
            }
        }
    }
}
*/