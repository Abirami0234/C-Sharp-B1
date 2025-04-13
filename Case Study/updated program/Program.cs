using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Net;


namespace CarConnect
{
    class Program
    {
        static List<Customer> customers = new List<Customer>();
        static List<Vehicle> vehicles = new List<Vehicle>();
        static List<Admin> admin = new List<Admin>();
        static List<Reservation> reservations = new List<Reservation>();

        static string connectionString = "Data Source=ABIRAMI;Initial Catalog=CarConnect;Integrated Security=True;TrustServerCertificate=True;";

        static void Main(string[] args)
        {
            LoadCustomers();
            LoadVehicles();
            

            while (true)
            {
                Console.WriteLine("\n===== MAIN MENU =====");
                Console.WriteLine("1. Customer Login");
                Console.WriteLine("2. Admin Login");
                Console.WriteLine("3. Register New Customer");
                Console.WriteLine("4. View Available Vehicles");
                Console.WriteLine("5. Exit");
                Console.Write("\nEnter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CustomerLogin();
                        break;
                    case "2":
                        AdminLogin();
                        break;
                    case "3":
                        RegisterCustomer();
                        break;
                    case "4":
                        ShowAvailableVehicles();
                        break;
                    case "5":
                        Console.WriteLine("Exiting... Thank you for using CarConnect!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice! Please try again.");
                        break;
                }
            }
        }

        static void LoadCustomers()
        {
            customers.Clear();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Customer";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Customer c = new Customer(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4),
                        reader.GetString(5),
                        reader.GetString(6),
                        reader.GetString(7),
                        reader.GetDateTime(8)
                    );
                    customers.Add(c);
                }
                reader.Close();
            }
        }

        static void LoadVehicles()
        {
            vehicles.Clear();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Vehicle";
                SqlCommand cmd = new SqlCommand(query, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Vehicle v = new Vehicle(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetInt32(3),
                            reader.GetString(4),
                            reader.GetString(5),
                            reader.GetBoolean(6),
                            reader.GetDecimal(7)
                        );
                        vehicles.Add(v);
                    }
                }
            }
        }
       

        static void CustomerLogin()
        {
            Console.Write("\nEnter Username: ");
            string username = Console.ReadLine();

            Customer customer = customers.Find(c => c.Username == username);
            if (customer == null)
            {
                Console.WriteLine("Customer not found.");
                return;
            }

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            if (customer.Authenticate(password))
            {
                Console.WriteLine($"Welcome, {customer.FirstName}!");
                ShowCustomerMenu(customer);
            }
            else
            {
                Console.WriteLine("Invalid password.");
            }
        }

        static void AdminLogin()
        {
            Console.Write("\nEnter Admin Username: ");
            string username = Console.ReadLine();

            Admin adminFromDb = LoadAdmin(username);

            if (adminFromDb == null)
            {
                Console.WriteLine("Admin not found.");
                return;
            }

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            if (adminFromDb.Authenticate(password))
            {
                Console.WriteLine($"Welcome, Admin {adminFromDb.FirstName}!");

                ShowAdminMenu(); 
            }
            else
            {
                Console.WriteLine("Incorrect admin password.");
            }
        }

        static Admin LoadAdmin(string username)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Admin WHERE Username = @Username";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Admin(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4),
                        reader.GetString(5),
                        reader.GetString(6),
                        reader.GetString(7),
                        reader.GetDateTime(8)
                    );
                }
            }
            return null;
        }

        static void RegisterCustomer()
        {
            Console.Write("\nFirst Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Phone: ");
            string phone = Console.ReadLine();

            Console.Write("City: ");
            string city = Console.ReadLine();

            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string insert = "INSERT INTO Customer (FirstName, LastName, Email, Phone, City, Username, Password, RegisteredDate) " +
                                "VALUES (@FirstName, @LastName, @Email, @Phone, @City, @Username, @Password, @RegisteredDate)";
                SqlCommand cmd = new SqlCommand(insert, conn);
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@City", city);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@RegisteredDate", DateTime.Now);
                cmd.ExecuteNonQuery();
            }

            LoadCustomers(); 
            Console.WriteLine("Customer registered successfully!");
        }

        static void ShowAvailableVehicles()
        {
            Console.WriteLine("\nAvailable Vehicles:");
            foreach (var v in vehicles)
            {
                if (v.IsAvailable)
                {
                    Console.WriteLine($"ID: {v.VehicleID} | {v.Make} {v.Model} - {v.Color} - {v.DailyRate}/day");
                }
            }
        }
        static void ShowCustomerMenu(Customer customer)
        {
            while (true)
            {
                Console.WriteLine("\n====== CUSTOMER DASHBOARD ======");
                Console.WriteLine("1. Make Reservation");
                Console.WriteLine("2. View My Reservations");
                Console.WriteLine("3. Cancel My Reservation");
                Console.WriteLine("4. Logout");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        MakeReservation(customer);
                        break;
                    case "2":
                        ViewCustomerReservations(customer);
                        break;
                    case "3":
                        CancelCustomerReservation(customer);
                        break;
                    case "4":
                        Console.WriteLine("Logging out...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        static void MakeReservation(Customer customer)
        {
            Console.WriteLine("\n--- Make Reservation ---");

            
            var availableVehicles = vehicles.Where(v => v.IsAvailable).ToList();
            if (availableVehicles.Count == 0)
            {
                Console.WriteLine("No available vehicles.");
                return;
            }

            Console.WriteLine("\nAvailable Vehicles:");
            foreach (var v in availableVehicles)
            {
                Console.WriteLine($"Vehicle ID: {v.VehicleID}, Make: {v.Make}, Model: {v.Model}, Rate/Day: {v.DailyRate}");
            }

            Console.Write("Enter Vehicle ID to reserve: ");
            int vehicleId = int.Parse(Console.ReadLine());

            var selectedVehicle = vehicles.FirstOrDefault(v => v.VehicleID == vehicleId && v.IsAvailable);
            if (selectedVehicle == null)
            {
                Console.WriteLine("Invalid or unavailable vehicle.");
                return;
            }

            Console.Write("Enter Start Date (yyyy-MM-dd): ");
            DateTime startDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter End Date (yyyy-MM-dd): ");
            DateTime endDate = DateTime.Parse(Console.ReadLine());

            if (endDate <= startDate)
            {
                Console.WriteLine("End date must be after start date.");
                return;
            }

            int days = (endDate - startDate).Days + 1;
            decimal totalCost = days * selectedVehicle.DailyRate;

            var reservation = new Reservation(
                id: 0, 
                customerId: customer.CustomerID,
                vehicleId: vehicleId,
                start: startDate,
                end: endDate,
                status: "Confirmed"
            );
            reservation.TotalCost = (double)totalCost;

            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string insertQuery = @"INSERT INTO Reservation (CustomerID, VehicleID, StartDate, EndDate, TotalCost, Status)
                               VALUES (@CustomerID, @VehicleID, @StartDate, @EndDate, @TotalCost, @Status)";
                SqlCommand cmd = new SqlCommand(insertQuery, conn);
                cmd.Parameters.AddWithValue("@CustomerID", reservation.CustomerID);
                cmd.Parameters.AddWithValue("@VehicleID", reservation.VehicleID);
                cmd.Parameters.AddWithValue("@StartDate", reservation.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", reservation.EndDate);
                cmd.Parameters.AddWithValue("@TotalCost", reservation.TotalCost);
                cmd.Parameters.AddWithValue("@Status", reservation.Status);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            
            LoadReservations(); 
            Console.WriteLine("Reservation successful!");
        }
        static List<Reservation> LoadReservations()
        {
            List<Reservation> reservations = new List<Reservation>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Reservation", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Reservation r = new Reservation
                    {
                        ReservationID = reader.GetInt32(0),
                        CustomerID = reader.GetInt32(1),
                        VehicleID = reader.GetInt32(2),
                        StartDate = reader.GetDateTime(3),
                        EndDate = reader.GetDateTime(4),
                        Status = reader.GetString(6)
                    };
                        
                    r.TotalCost = Convert.ToDouble(reader["TotalCost"]);
                    reservations.Add(r);
                }
            }

            return reservations;
        }

        static void ViewCustomerReservations(Customer customer)
        {
            reservations = LoadReservations();

            Console.WriteLine($"Logged in Customer ID: {customer.CustomerID}");
            

            var myReservations = reservations.Where(r => r.CustomerID == customer.CustomerID).ToList();

            
            Console.WriteLine("\n--- My Reservations ---");
            if (myReservations.Count == 0)
            {
                Console.WriteLine("You have no reservations.");
                return;
            }

            foreach (var r in myReservations)
            {
                Console.WriteLine($"Reservation ID: {r.ReservationID}, Vehicle ID: {r.VehicleID}, " +
                                  $"Start: {r.StartDate.ToShortDateString()}, End: {r.EndDate.ToShortDateString()}, " +
                                  $"Total: {r.TotalCost}, Status: {r.Status}");
            }
        }
        static void CancelCustomerReservation(Customer customer)
        {
            Console.WriteLine("\n--- Cancel Reservation ---");

            
            var myReservations = reservations
                .Where(r => r.CustomerID == customer.CustomerID && r.Status.ToLower() == "confirmed")
                .ToList();

            if (myReservations.Count == 0)
            {
                Console.WriteLine("You have no active (confirmed) reservations to cancel.");
                return;
            }

            
            foreach (var r in myReservations)
            {
                Console.WriteLine($"Reservation ID: {r.ReservationID}, Vehicle ID: {r.VehicleID}, " +
                                  $"Start: {r.StartDate.ToShortDateString()}, End: {r.EndDate.ToShortDateString()}, " +
                                  $"Total: ₹{r.TotalCost}, Status: {r.Status}");
            }

            Console.Write("Enter Reservation ID to cancel: ");
            if (!int.TryParse(Console.ReadLine(), out int reservationId))
            {
                Console.WriteLine("Invalid input.");
                return;
            }

            
            var reservationToCancel = myReservations.FirstOrDefault(r => r.ReservationID == reservationId);
            if (reservationToCancel == null)
            {
                Console.WriteLine("Reservation not found or not allowed to cancel.");
                return;
            }

            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string updateQuery = "UPDATE Reservation SET Status = 'Cancelled' WHERE ReservationID = @ReservationID";
                SqlCommand cmd = new SqlCommand(updateQuery, conn);
                cmd.Parameters.AddWithValue("@ReservationID", reservationId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            
            LoadReservations();

            Console.WriteLine("Reservation cancelled successfully.");
        }


        static void ShowAdminMenu()
        {
            while (true)
            {
                Console.WriteLine("\n====== ADMIN DASHBOARD ======");
                Console.WriteLine("1. Manage Customers");
                Console.WriteLine("2. Manage Vehicles");
                Console.WriteLine("3. Manage Reservations");
                Console.WriteLine("4. Generate Reports");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowCustomerManagementMenu();
                        break;
                    case "2":
                        ShowVehicleManagementMenu();
                        break;
                    case "3":
                        ShowReservationManagementMenu();
                        break;
                    case "4":
                        ShowReportMenu();
                        break;
                    case "5":
                        Console.WriteLine("Exiting admin dashboard...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice! Please try again.");
                        break;
                }
            }
        }

        static void ShowCustomerManagementMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- CUSTOMER MANAGEMENT ---");
                Console.WriteLine("1. View All Customers");
                Console.WriteLine("2. View Customer by ID");
                Console.WriteLine("3. Update Customer");
                Console.WriteLine("4. Delete Customer");
                Console.WriteLine("5. Back to Admin Menu");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ViewAllCustomers();
                        break;
                    case "2":
                        ViewCustomerById();
                        break;
                    case "3":
                        UpdateCustomer();
                        break;
                    case "4":
                        DeleteCustomer();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
        static void ViewAllCustomers()
        {
            Console.WriteLine("\n--- All Customers ---");
            foreach (var c in customers)
            {
                Console.WriteLine($"ID: {c.CustomerID}, Name: {c.FirstName} {c.LastName}, Email: {c.Email}, Phone: {c.PhoneNumber}, City: {c.Address}");
            }
        }

        static void ViewCustomerById()
        {
            Console.Write("Enter Customer ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var customer = customers.Find(c => c.CustomerID == id);
                if (customer != null)
                {
                    Console.WriteLine($"ID: {customer.CustomerID}\nName: {customer.FirstName} {customer.LastName}\nEmail: {customer.Email}\nPhone: {customer.PhoneNumber}\nCity: {customer.Address}\nRegistered On: {customer.RegistrationDate}");
                }
                else
                {
                    Console.WriteLine("Customer not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }

        static void UpdateCustomer()
        {
            Console.Write("Enter Customer ID to Update: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var customer = customers.Find(c => c.CustomerID == id);
                if (customer == null)
                {
                    Console.WriteLine("Customer not found.");
                    return;
                }

                Console.Write("New First Name (leave blank to keep unchanged): ");
                string firstName = Console.ReadLine();
                Console.Write("New Last Name (leave blank to keep unchanged): ");
                string lastName = Console.ReadLine();
                Console.Write("New Email: ");
                string email = Console.ReadLine();
                Console.Write("New Phone: ");
                string phone = Console.ReadLine();
                Console.Write("New City: ");
                string address = Console.ReadLine();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"UPDATE Customer 
                             SET FirstName = @FirstName, LastName = @LastName, Email = @Email, PhoneNumber = @phone, Address = @address 
                             WHERE CustomerID = @CustomerID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@FirstName", string.IsNullOrWhiteSpace(firstName) ? customer.FirstName : firstName);
                    cmd.Parameters.AddWithValue("@LastName", string.IsNullOrWhiteSpace(lastName) ? customer.LastName : lastName);
                    cmd.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(email) ? customer.Email : email);
                    cmd.Parameters.AddWithValue("@phone", string.IsNullOrWhiteSpace(phone) ? customer.PhoneNumber : phone);
                    cmd.Parameters.AddWithValue("@address", string.IsNullOrWhiteSpace(address) ? customer.Address : address);
                    cmd.Parameters.AddWithValue("@CustomerID", id);
                    cmd.ExecuteNonQuery();
                }

                LoadCustomers();
                Console.WriteLine("Customer updated successfully.");
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }

        static void DeleteCustomer()
        {
            Console.Write("Enter Customer ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Customer WHERE CustomerID = @CustomerID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CustomerID", id);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Customer deleted successfully.");
                        LoadCustomers();
                    }
                    else
                    {
                        Console.WriteLine("Customer not found.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }

        static void ShowVehicleManagementMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- VEHICLE MANAGEMENT ---");
                Console.WriteLine("1. View All Vehicles");
                Console.WriteLine("2. Add New Vehicle");
                Console.WriteLine("3. Update Vehicle");
                Console.WriteLine("4. Remove Vehicle");
                Console.WriteLine("5. Back to Admin Menu");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": ViewAllVehicles(); break;
                    case "2": AddNewVehicle(); break;
                    case "3": UpdateVehicle(); break;
                    case "4": RemoveVehicle(); break;
                    case "5": return;
                    default: Console.WriteLine("Invalid choice."); break;
                }
            }

        }
        static void ViewAllVehicles()
        {
            Console.WriteLine("\n--- All Vehicles ---");
            foreach (var v in vehicles)
            {
                Console.WriteLine($"ID: {v.VehicleID}, Make: {v.Make}, Model: {v.Model}, Type: {v.Type}, Year: {v.Year},  Rent: {v.DailyRate}, Status: {v.IsAvailable}");
            }
        }

        static void AddNewVehicle()
        {
            Console.WriteLine("\n--- Add New Vehicle ---");
            Console.Write("Make: ");
            string make = Console.ReadLine();
            Console.Write("Model: ");
            string model = Console.ReadLine();
            Console.Write("Type (SUV, Sedan, etc): ");
            string type = Console.ReadLine();
            Console.Write("Manufacture Year: ");
            int year = int.Parse(Console.ReadLine());
            Console.Write("Rent per Day: ");
            decimal rent = decimal.Parse(Console.ReadLine());
            Console.Write("Availability Status (Available/Unavailable): ");
            string status = Console.ReadLine();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"INSERT INTO Vehicle (Make, Model, Type, Year, FuelType, SeatingCapacity, RentPerDay, AvailabilityStatus) 
                         VALUES (@Make, @Model, @Type, @Year, @FuelType, @SeatingCapacity, @RentPerDay, @AvailabilityStatus)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Make", make);
                cmd.Parameters.AddWithValue("@Model", model);
                cmd.Parameters.AddWithValue("@Type", type);
                cmd.Parameters.AddWithValue("@Year", year);
                cmd.Parameters.AddWithValue("@RentPerDay", rent);
                cmd.Parameters.AddWithValue("@AvailabilityStatus", status);
                cmd.ExecuteNonQuery();
            }

            LoadVehicles();
            Console.WriteLine("Vehicle added successfully.");
        }

        static void UpdateVehicle()
        {
            Console.Write("\nEnter Vehicle ID to Update: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var vehicle = vehicles.Find(v => v.VehicleID == id);
                if (vehicle == null)
                {
                    Console.WriteLine("Vehicle not found.");
                    return;
                }

                Console.Write("New Brand (leave blank to keep unchanged): ");
                string make = Console.ReadLine();
                Console.Write("New Model: ");
                string model = Console.ReadLine();
                Console.Write("New Year: ");
                string yearInput = Console.ReadLine();                
                Console.Write("New Rent Per Day: ");
                string DailyRent = Console.ReadLine();
                Console.Write("New Availability Status: ");
                string status = Console.ReadLine();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"UPDATE Vehicle 
                 SET Make = @Make, 
                     Model = @Model, 
                     Year = @Year,                                  
                     DailyRate = @DailyRate, 
                     IsAvailable = @IsAvailable 
                 WHERE VehicleID = @VehicleID";
                    
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Make", string.IsNullOrWhiteSpace(make) ? vehicle.Make : make);
                    cmd.Parameters.AddWithValue("@Model", string.IsNullOrWhiteSpace(model) ? vehicle.Model : model);                    
                    cmd.Parameters.AddWithValue("@Year", string.IsNullOrWhiteSpace(yearInput) ? vehicle.Year : int.Parse(yearInput));
                    cmd.Parameters.AddWithValue("@DailyRate", string.IsNullOrWhiteSpace(DailyRent) ? vehicle.DailyRate : decimal.Parse(DailyRent));
                    cmd.Parameters.AddWithValue("@IsAvailable", string.IsNullOrWhiteSpace(status) ? vehicle.IsAvailable : bool.Parse(status));
                    cmd.Parameters.AddWithValue("@VehicleID", id);
                    cmd.ExecuteNonQuery();
                }

                LoadVehicles();
                Console.WriteLine("Vehicle updated successfully.");
            }
            else
            {
                Console.WriteLine("Invalid Vehicle ID.");
            }
        }

        static void RemoveVehicle()
        {
            Console.Write("\nEnter Vehicle ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Vehicle WHERE VehicleID = @VehicleID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@VehicleID", id);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Vehicle deleted successfully.");
                        LoadVehicles();
                    }
                    else
                    {
                        Console.WriteLine("Vehicle not found.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid Vehicle ID.");
            }
        }

        

        

        static void ShowReservationManagementMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- RESERVATION MANAGEMENT ---");
                Console.WriteLine("1. View All Reservations");
                Console.WriteLine("2. View Reservation by ID");
                Console.WriteLine("3. Update Reservation");
                Console.WriteLine("4. Cancel Reservation");
                Console.WriteLine("5. Back to Admin Menu");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        
                        ViewAllReservations(); break;
                    case "2": ViewReservationById(); break;
                    case "3": UpdateReservation(); break;
                    case "4": CancelReservation(); break;
                    case "5": return;
                    default: Console.WriteLine("Invalid choice."); break;
                }
            }

        }
        static void ViewAllReservations()
        {
            reservations = LoadReservations();
            Console.WriteLine("\n--- All Reservations ---");

            if (reservations.Count == 0)
            {
                Console.WriteLine("No reservations found.");
                return;
            }

            foreach (var r in reservations)
            {
                Console.WriteLine($"ID: {r.ReservationID}, CustomerID: {r.CustomerID}, VehicleID: {r.VehicleID}, " +
                                  $"Start: {r.StartDate.ToShortDateString()}, End: {r.EndDate.ToShortDateString()}, " +
                                  $"Total Cost: {r.TotalCost}, Status: {r.Status}");
            }
        }
        static void ViewReservationById()
        {
            Console.Write("\nEnter Reservation ID to view: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var r = reservations.FirstOrDefault(res => res.ReservationID == id);
                if (r != null)
                {
                    Console.WriteLine($"Reservation ID: {r.ReservationID}");
                    Console.WriteLine($"Customer ID: {r.CustomerID}");
                    Console.WriteLine($"Vehicle ID: {r.VehicleID}");
                    Console.WriteLine($"Start Date: {r.StartDate}");
                    Console.WriteLine($"End Date: {r.EndDate}");
                    Console.WriteLine($"Total Cost: {r.TotalCost}");
                    Console.WriteLine($"Status: {r.Status}");
                }
                else
                {
                    Console.WriteLine("Reservation not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }
        static void UpdateReservation()
        {
            Console.Write("\nEnter Reservation ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            
            Reservation reservation = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Reservation WHERE ReservationID = @ReservationID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ReservationID", id);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    reservation = new Reservation
                    {
                        ReservationID = (int)reader["ReservationID"],
                        CustomerID = (int)reader["CustomerID"],
                        VehicleID = (int)reader["VehicleID"],
                        StartDate = (DateTime)reader["StartDate"],
                        EndDate = (DateTime)reader["EndDate"],
                        Status = reader["Status"].ToString()
                    };
                    reservation.TotalCost = Convert.ToDouble(reader["TotalCost"]);
                }   
                
            }

            if (reservation == null)
            {
                Console.WriteLine("Reservation not found.");
                return;
            }

            Console.Write($"New Start Date (yyyy-mm-dd) [Current: {reservation.StartDate:yyyy-MM-dd}]: ");
            string startInput = Console.ReadLine();
            Console.Write($"New End Date (yyyy-mm-dd) [Current: {reservation.EndDate:yyyy-MM-dd}]: ");
            string endInput = Console.ReadLine();
            Console.Write($"New Status (Active/Cancelled) [Current: {reservation.Status}]: ");
            string statusInput = Console.ReadLine();

            DateTime newStartDate = string.IsNullOrWhiteSpace(startInput) ? reservation.StartDate : DateTime.Parse(startInput);
            DateTime newEndDate = string.IsNullOrWhiteSpace(endInput) ? reservation.EndDate : DateTime.Parse(endInput);
            string newStatus = string.IsNullOrWhiteSpace(statusInput) ? reservation.Status : statusInput;

            
            decimal rentPerDay = vehicles.FirstOrDefault(v => v.VehicleID == reservation.VehicleID)?.DailyRate ?? 0;
            int days = (newEndDate - newStartDate).Days + 1;
            decimal newTotalCost = rentPerDay * days;

            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"UPDATE Reservation 
                         SET StartDate = @StartDate, EndDate = @EndDate, 
                             TotalCost = @TotalCost, Status = @Status 
                         WHERE ReservationID = @ReservationID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@StartDate", newStartDate);
                cmd.Parameters.AddWithValue("@EndDate", newEndDate);
                cmd.Parameters.AddWithValue("@TotalCost", newTotalCost);
                cmd.Parameters.AddWithValue("@Status", newStatus);
                cmd.Parameters.AddWithValue("@ReservationID", id);
                cmd.ExecuteNonQuery();
            }

            Console.WriteLine("Reservation updated successfully.");

            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Reservation WHERE ReservationID = @ReservationID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ReservationID", id);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Console.WriteLine("\n--- Updated Reservation ---");
                    Console.WriteLine($"Reservation ID: {reader["ReservationID"]}");
                    Console.WriteLine($"Customer ID: {reader["CustomerID"]}");
                    Console.WriteLine($"Vehicle ID: {reader["VehicleID"]}");
                    Console.WriteLine($"Start Date: {Convert.ToDateTime(reader["StartDate"]).ToShortDateString()}");
                    Console.WriteLine($"End Date: {Convert.ToDateTime(reader["EndDate"]).ToShortDateString()}");
                    Console.WriteLine($"Total Cost: {reader["TotalCost"]}");
                    Console.WriteLine($"Status: {reader["Status"]}");
                }
            }

            
            LoadReservations();
        }

        static void CancelReservation()
        {
            Console.Write("\nEnter Reservation ID to cancel: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE Reservation SET Status = 'Cancelled' WHERE ReservationID = @ReservationID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ReservationID", id);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        LoadReservations();
                        Console.WriteLine("Reservation cancelled successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Reservation not found.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }


        static void ShowReportMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- REPORT GENERATION ---");
                Console.WriteLine("1. Daily Reservations Report");
                Console.WriteLine("2. Revenue Report");
                Console.WriteLine("3. Vehicle Utilization Report");
                Console.WriteLine("4. Back to Admin Menu");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        GenerateDailyReservationsReport();
                        break;
                    case "2":
                        GenerateRevenueReport();
                        break;
                    case "3":
                        GenerateVehicleUtilizationReport();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
        static void GenerateDailyReservationsReport()
        {
            Console.Write("Enter date (yyyy-mm-dd): ");
            string dateInput = Console.ReadLine();
            if (!DateTime.TryParse(dateInput, out DateTime date))
            {
                Console.WriteLine("Invalid date format.");
                return;
            }

            var reservationsForDate = reservations
                .Where(r => r.StartDate.Date == date.Date)
                .ToList();

            if (reservationsForDate.Count == 0)
            {
                Console.WriteLine("No reservations found for the selected date.");
                return;
            }

            Console.WriteLine($"\nReservations for {date.ToShortDateString()}:");
            foreach (var res in reservationsForDate)
            {
                Console.WriteLine($"Reservation ID: {res.ReservationID}, Customer ID: {res.CustomerID}, Vehicle ID: {res.VehicleID}, Start: {res.StartDate.ToShortDateString()}, End: {res.EndDate.ToShortDateString()}, Amount: {res.TotalCost}");
            }
        }


        static void GenerateRevenueReport()
        {
            Console.Write("Enter start date (yyyy-mm-dd): ");
            string startInput = Console.ReadLine();
            Console.Write("Enter end date (yyyy-mm-dd): ");
            string endInput = Console.ReadLine();

            if (!DateTime.TryParse(startInput, out DateTime startDate) || !DateTime.TryParse(endInput, out DateTime endDate))
            {
                Console.WriteLine("Invalid date format.");
                return;
            }

            var filteredReservations = reservations
                .Where(r => r.StartDate.Date >= startDate.Date && r.StartDate.Date <= endDate.Date)
                .ToList();

            double totalRevenue = filteredReservations.Sum(r => r.TotalCost);

            Console.WriteLine($"\nTotal revenue from {startDate.ToShortDateString()} to {endDate.ToShortDateString()} is {totalRevenue}");
        }

        static void GenerateVehicleUtilizationReport()
        {
            Dictionary<int, int> vehicleUsageCount = new Dictionary<int, int>();

            foreach (var reservation in reservations)
            {
                if (vehicleUsageCount.ContainsKey(reservation.VehicleID))
                    vehicleUsageCount[reservation.VehicleID]++;
                else
                    vehicleUsageCount[reservation.VehicleID] = 1;
            }

            Console.WriteLine("\n--- Vehicle Utilization Report ---");
            foreach (var vehicle in vehicles)
            {
                int usageCount = vehicleUsageCount.ContainsKey(vehicle.VehicleID) ? vehicleUsageCount[vehicle.VehicleID] : 0;
                Console.WriteLine($"Vehicle ID: {vehicle.VehicleID}, Model: {vehicle.Model}, Times Rented: {usageCount}");
            }
        }

    }
}
