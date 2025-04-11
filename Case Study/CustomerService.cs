using System.Collections.Generic;
using System.Linq;

namespace CarConnect
{
    public class CustomerService : ICustomerService
    {
        private List<Customer> customers = new List<Customer>();

        public Customer GetCustomerById(int customerId) =>
            customers.FirstOrDefault(c => c.CustomerID == customerId);

        public Customer GetCustomerByUsername(string username) =>
            customers.FirstOrDefault(c => c.Username == username);

        public void RegisterCustomer(Customer customer) =>
            customers.Add(customer);

        public void UpdateCustomer(Customer customer)
        {
            var existing = GetCustomerById(customer.CustomerID);
            if (existing != null)
            {
                customers.Remove(existing);
                customers.Add(customer);
            }
        }

        public void DeleteCustomer(int customerId) =>
            customers.RemoveAll(c => c.CustomerID == customerId);
    }
}
