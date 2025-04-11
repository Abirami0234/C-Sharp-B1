using System.Collections.Generic;
using System.Linq;


namespace CarConnect
{
    public class VehicleService : IVehicleService
    {
        public List<Vehicle> GetAllVehicles()
        {
            return vehicles;
        }

        private List<Vehicle> vehicles = new List<Vehicle>();


        public Vehicle GetVehicleById(int vehicleId) =>
            vehicles.FirstOrDefault(v => v.VehicleID == vehicleId);


        public List<Vehicle> GetAvailableVehicles()
        {
            return vehicles.Where(v => v.IsAvailable).ToList();
        }



        public void AddVehicle(Vehicle vehicle) =>
            vehicles.Add(vehicle);

        public void UpdateVehicle(Vehicle vehicle)
        {
            var existing = GetVehicleById(vehicle.VehicleID);
            if (existing != null)
            {
                vehicles.Remove(existing);
                vehicles.Add(vehicle);
            }
        }

        public void RemoveVehicle(int vehicleId) =>
            vehicles.RemoveAll(v => v.VehicleID == vehicleId);
    }
}
