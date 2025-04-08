using System;
using System.Text.RegularExpressions;

namespace Banking_System
{
    public class task10_Customer
    {
        public long CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        private string email;
        private string phone;
        public string Address { get; set; }

        public task10_Customer() { }

        public task10_Customer(long id, string firstName, string lastName, string email, string phone, string address)
        {
            CustomerID = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Address = address;
        }

        public string Email
        {
            get => email;
            set
            {
                if (Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                    email = value;
                else
                    throw new ArgumentException("Invalid email format.");
            }
        }

        public string Phone
        {
            get => phone;
            set
            {
                if (Regex.IsMatch(value, @"^\d{10}$"))
                    phone = value;
                else
                    throw new ArgumentException("Phone must be a 10-digit number.");
            }
        }

        public void PrintCustomerInfo()
        {
            Console.WriteLine($"Customer ID: {CustomerID}, Name: {FirstName} {LastName}, Email: {Email}, Phone: {Phone}, Address: {Address}");
        }
    }
}
