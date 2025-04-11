using System;

namespace CarConnect
{
    public class AuthenticationService
    {
        private readonly ICustomerService _customerService;
        private readonly IAdminService _adminService;

        public AuthenticationService(ICustomerService customerService, IAdminService adminService)
        {
            _customerService = customerService;
            _adminService = adminService;
        }

        public Customer AuthenticateCustomer(string username, string password)
        {
            var customer = _customerService.GetCustomerByUsername(username);
            if (customer == null || !customer.Authenticate(password))
            {
                throw new AuthenticationException("Invalid customer credentials.");
            }
            return customer;
        }

        public Admin AuthenticateAdmin(string username, string password)
        {
            var admin = _adminService.GetAdminByUsername(username);
            if (admin == null || !admin.Authenticate(password))
            {
                throw new AuthenticationException("Invalid admin credentials.");
            }
            return admin;
        }
    }
}
