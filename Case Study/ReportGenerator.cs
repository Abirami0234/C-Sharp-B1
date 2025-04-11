using System;
using System.Collections.Generic;
using System.Linq;

namespace CarConnect
{
    public class ReportGenerator
    {
        public static void GenerateVehicleAvailabilityReport(List<Vehicle> vehicles)
        {
            Console.WriteLine("\n=== Vehicle Availability Report ===");
            foreach (var v in vehicles)
            {
                string status = v.IsAvailable ? "Available" : "Reserved";
                Console.WriteLine($"ID: {v.VehicleID}, {v.Make},{v.Model} ,({v.Type} - {status}");
            }
        }

        public static void GenerateReservationSummaryReport(List<Reservation> reservations)
        {
            Console.WriteLine("\n=== Reservation Summary Report ===");
            foreach (var r in reservations)
            {
                Console.WriteLine($"Reservation ID: {r.ReservationID}, Vehicle ID: {r.VehicleID}, Customer ID: {r.CustomerID}, From: {r.StartDate.ToShortDateString()} To: {r.EndDate.ToShortDateString()}");
            }
        }

        public static void GenerateRevenueReport(List<Reservation> reservations, List<Vehicle> vehicles)
        {
            Console.WriteLine("\n=== Revenue Report ===");
            decimal totalRevenue = 0;
            foreach (var r in reservations)
            {
                var vehicle = vehicles.FirstOrDefault(v => v.VehicleID == r.VehicleID);
                if (vehicle != null)
                {
                    int days = (r.EndDate - r.StartDate).Days;
                    decimal revenue = vehicle.DailyRate * days;
                    totalRevenue += revenue;
                    Console.WriteLine($"Reservation ID: {r.ReservationID}, Revenue: ₹{revenue}");
                }
            }
            Console.WriteLine($"Total Revenue: ₹{totalRevenue}");
        }
    }
}
