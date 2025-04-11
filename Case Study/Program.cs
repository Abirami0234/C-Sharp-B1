using System;
using CarConnect;


namespace CarConnectApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to CarConnect Car Rental System\n");

            
            Customer customer = new Customer(1, "Abirami", "M", "abi@example.com", "9876543210", "Dindigul", "abiuser", "secure123", DateTime.Now);
            Console.WriteLine("Customer: " + customer.FirstName + " " + customer.LastName);

           
            Console.WriteLine("Authentication: " + (customer.Authenticate("secure123") ? "Success" : "Failure"));

            
            Vehicle car = new Vehicle(1, "Innova", "Toyota", 2023, "White", "TN01X1234", true, 1500);
            Console.WriteLine("Vehicle: " + car.Model + " by " + car.Make);

            
            Reservation reservation = new Reservation(101, customer.CustomerID, car.VehicleID, DateTime.Now, DateTime.Now.AddDays(3), "Pending");
            Console.WriteLine("Total Cost for reservation: Rs." + reservation.TotalCost);

           
            Admin admin = new Admin(1, "Admin", "One", "admin@example.com", "9876543211", "admin1", "adminpass", "SuperAdmin", DateTime.Now);
            Console.WriteLine("Admin Authenticated: " + (admin.Authenticate("adminpass") ? "Yes" : "No"));

            Console.WriteLine("\nSystem check complete.");
        }
    }
}
