using System;

namespace CareerHub
{
    class task_4
    {
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("\n=== Task 4: Database Connectivity Demo ===");
                Console.WriteLine("1. Retrieve Job Listings");
                Console.WriteLine("2. Create Applicant Profile");
                Console.WriteLine("3. Submit Job Application");
                Console.WriteLine("4. Post a New Job");
                Console.WriteLine("5. Search Jobs by Salary Range");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        JobListingRetrieval.RetrieveAllJobListings();
                        break;
                    case "2":
                        ApplicantProfileCreation.CreateNewApplicantProfile();
                        break;
                    case "3":
                        JobApplicationSubmission.SubmitNewJobApplication();
                        break;
                    case "4":
                        CompanyJobPosting.PostNewCompanyJob();
                        break;
                    case "5":
                        SalaryRangeQuery.RunSalaryRangeQuery();
                        break;
                    case "6":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                Console.WriteLine();
            }
        }
    }
}
