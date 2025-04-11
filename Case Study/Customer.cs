using System;

namespace CarConnect
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }

        public Customer() { }

        public Customer(int id, string firstName, string lastName, string email, string phone, string address, string username, string password, DateTime regDate)
        {
            CustomerID = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phone;
            Address = address;
            Username = username;
            Password = password;
            RegistrationDate = regDate;
        }

        public bool Authenticate(string password)
        {
            return Password == password; 
        }
    }
}
