/*using System;
using System.Collections.Generic;

namespace CareerHub
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseManager repo = new DatabaseManager(); // Assuming this contains all your DB methods

            while (true)
            {
                Console.WriteLine("\n===== CareerHub Menu =====");
                Console.WriteLine("1. List all Job Listings");
                Console.WriteLine("2. List all Companies");
                Console.WriteLine("3. List all Applicants");
                Console.WriteLine("4. Get Applications for a Job");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice (1-5): ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        var jobs = repo.GetJobListings();
                        Console.WriteLine("Job Listings:");
                        foreach (var job in jobs)
                        {
                            Console.WriteLine($"ID: {job.JobID}, Title: {job.JobTitle}, Type: {job.JobType}, Location: {job.JobLocation}, Salary: {job.Salary:C}");
                        }
                        break;

                    case "2":
                        var companies = repo.GetCompanies();
                        Console.WriteLine("Companies:");
                        foreach (var company in companies)
                        {
                            Console.WriteLine($"ID: {company.CompanyID}, Name: {company.CompanyName}, Location: {company.Location}");
                        }
                        break;

                    case "3":
                        var applicants = repo.GetApplicants();
                        Console.WriteLine("Applicants:");
                        foreach (var applicant in applicants)
                        {
                            Console.WriteLine($"ID: {applicant.ApplicantID}, Name: {applicant.FirstName} {applicant.LastName}, Email: {applicant.Email}, City: {applicant.City}, State: {applicant.State}");
                        }
                        break;

                    case "4":
                        Console.Write("Enter Job ID: ");
                        if (int.TryParse(Console.ReadLine(), out int jobID))
                        {
                            var applications = repo.GetApplicationsForJob(jobID);
                            if (applications.Count == 0)
                            {
                                Console.WriteLine("No applications found for this job.");
                            }
                            else
                            {
                                Console.WriteLine("Applications:");
                                foreach (var app in applications)
                                {
                                    Console.WriteLine($"ApplicationID: {app.ApplicationID}, ApplicantID: {app.ApplicantID}, Cover Letter: {app.CoverLetter}");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid job ID.");
                        }
                        break;

                    case "5":
                        Console.WriteLine("Exiting application. Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a number from 1 to 5.");
                        break;
                }
            }
        }
    }
}
*/