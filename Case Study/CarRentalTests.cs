using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CarConnect
{
    [TestFixture]
    public class CarRentalSystemTests
    {
        private CustomerService customerService;
        private VehicleService vehicleService;

        [SetUp]
        public void Setup()
        {
            customerService = new CustomerService();
            vehicleService = new VehicleService();
        }

        [Test]
        public void TestCustomerAuthenticationWithInvalidCredentials()
        {
            var customer = new Customer(1, "Abi", "M", "abi@email.com", "9999999999", "Dindigul", "abi123", "correctpass", DateTime.Now);
            customerService.RegisterCustomer(customer);

            var fetched = customerService.GetCustomerByUsername("abi123");
            NUnit.Framework.Assert.IsFalse(fetched.Authenticate("wrongpass"));
        }

        [Test]
        public void TestUpdateCustomerInformation()
        {
            var customer = new Customer(2, "Ram", "K", "ram@email.com", "8888888888", "Chennai", "ram123", "pass", DateTime.Now);
            customerService.RegisterCustomer(customer);

            customer.Email = "new@email.com";
            customerService.UpdateCustomer(customer);

            var updated = customerService.GetCustomerById(2);
            NUnit.Framework.Assert.AreEqual("new@email.com", updated.Email);
        }

        [Test]
        public void TestAddNewVehicle()
        {
            var vehicle = new Vehicle(101, "i20", "Hyundai", 2022, "White", "TN09X0001", true, 1500.00m);
            vehicleService.AddVehicle(vehicle);

            var fetched = vehicleService.GetVehicleById(101);
            NUnit.Framework.Assert.IsNotNull(fetched);
            NUnit.Framework.Assert.AreEqual("i20", fetched.Model);
        }

        [Test]
        public void TestUpdateVehicleDetails()
        {
            var vehicle = new Vehicle(102, "Alto", "Maruti", 2020, "Red", "TN10Y1111", true, 1000.00m);
            vehicleService.AddVehicle(vehicle);

            vehicle.Color = "Blue";
            vehicle.DailyRate = 1100.00m;
            vehicleService.UpdateVehicle(vehicle);

            var updated = vehicleService.GetVehicleById(102);
            NUnit.Framework.Assert.AreEqual("Blue", updated.Color);
            NUnit.Framework.Assert.AreEqual(1100.00m, updated.DailyRate);
        }

        [Test]
       
        public void TestGetAvailableVehicles()
        {
           
            vehicleService.AddVehicle(new Vehicle(201, "Swift", "Maruti", 2021, "Grey", "TN12Z1212", true, 1300.00m));
            vehicleService.AddVehicle(new Vehicle(202, "City", "Honda", 2019, "Black", "TN13A2323", false, 1800.00m));

            
            var available = vehicleService.GetAvailableVehicles();

            Console.WriteLine("Available count: " + available.Count);
            foreach (var v in available)
                Console.WriteLine($"ID: {v.VehicleID}, Available: {v.IsAvailable}, Model: {v.Model}");
            
            Assert.IsTrue(available.Exists(v => v.VehicleID == 201));
            Assert.IsFalse(available.Exists(v => v.VehicleID == 202));
        }

        [Test]
        public void TestGetAllVehicles()
        {
            vehicleService.AddVehicle(new Vehicle(301, "Baleno", "Suzuki", 2022, "Silver", "TN14B3434", true, 1600.00m));
            vehicleService.AddVehicle(new Vehicle(302, "Verna", "Hyundai", 2023, "Blue", "TN15C4545", true, 1900.00m));

            var all = vehicleService.GetAllVehicles();

            NUnit.Framework.Assert.AreEqual(2, all.Count);
        }
    }
}
