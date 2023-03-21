using Microsoft.AspNetCore.Identity;
using TimeManagerPortalReact.Data;
using TimeManagerPortalReact.Interfaces;

namespace TimeManagerPortalReact.Reporitories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public ICollection<IdentityRole> GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
