using Microsoft.AspNetCore.Identity;

namespace TimeManagerMVC.Interfaces
{
    public interface IRoleRepository
    {
        ICollection<IdentityRole> GetRoles();
    }
}
