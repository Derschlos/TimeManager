using Microsoft.AspNetCore.Identity;

namespace TimeManagerPortalReact.Interfaces
{
    public interface IRoleRepository
    {
        ICollection<IdentityRole> GetRoles();
    }
}
