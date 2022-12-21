using Microsoft.AspNetCore.Identity;
using TimeManagerMVC.Data;
using TimeManagerMVC.Interfaces;

namespace TimeManagerMVC.Reporitories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly TimeManagerUserContext _context;

        public RoleRepository(TimeManagerUserContext context)
        {
            _context = context;
        }
        public ICollection<IdentityRole> GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
