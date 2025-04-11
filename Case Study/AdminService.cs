using System.Collections.Generic;
using System.Linq;


namespace CarConnect
{
    public class AdminService : IAdminService
    {
        private List<Admin> admins = new List<Admin>();

        public Admin GetAdminById(int adminId) =>
            admins.FirstOrDefault(a => a.AdminID == adminId);

        public Admin GetAdminByUsername(string username) =>
            admins.FirstOrDefault(a => a.Username == username);

        public void RegisterAdmin(Admin admin) =>
            admins.Add(admin);

        public void UpdateAdmin(Admin admin)
        {
            var existing = GetAdminById(admin.AdminID);
            if (existing != null)
            {
                admins.Remove(existing);
                admins.Add(admin);
            }
        }

        public void DeleteAdmin(int adminId) =>
            admins.RemoveAll(a => a.AdminID == adminId);
    }
}
