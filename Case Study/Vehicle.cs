using System;
using System.Collections.Generic;

namespace CarConnect
{
    public class Vehicle
    {
        public int VehicleID { get; set; }
        public string Model { get; set; }
        public string Make { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string RegistrationNumber { get; set; }
        public bool IsAvailable { get; set; }
        public decimal DailyRate { get; set; }

        public Vehicle() { }

        
        

        public string Type
        {
            get { return Year + " " + Make; }
        }
        public Vehicle(int id, string model, string make, int year, string color, string regNo, bool available, decimal rate)
        {
            VehicleID = id;
            Model = model;
            Make = make;
            Year = year;
            Color = color;
            RegistrationNumber = regNo;
            IsAvailable = available; 
            DailyRate = rate;
        }
    }
}