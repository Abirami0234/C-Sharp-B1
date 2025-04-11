using System;

namespace CarConnect
{
    public class Reservation
    {
        public int ReservationID { get; set; }
        public int CustomerID { get; set; }
        public int VehicleID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TotalCost { get; set; }
        public string Status { get; set; }

        public Reservation() { }

        public Reservation(int id, int customerId, int vehicleId, DateTime start, DateTime end, string status)
        {
            ReservationID = id;
            CustomerID = customerId;
            VehicleID = vehicleId;
            StartDate = start;
            EndDate = end;
            Status = status;
            TotalCost = CalculateTotalCost();
        }

        public double CalculateTotalCost()
        {
            TimeSpan duration = EndDate - StartDate;
            return duration.TotalDays * 1000; 
        }
    }
}
