using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TimeManagerMVC.Data
{
    public class SeedData
    {
        internal static void InitializeRolesData(IServiceProvider serviceProvider)
        {
            using (var context = new TimeManagerUserContext(
                serviceProvider.GetRequiredService<DbContextOptions<TimeManagerUserContext>>()
                ))
            {
                if (context.Roles.Any())
                {
                    return; //Db has been seeded
                }
                context.Roles.AddRange(
                      new IdentityRole
                      {
                          Name = "Admin",
                          NormalizedName = "ADMIN",
                      },
                      new IdentityRole
                      {
                          Name = "Manager",
                          NormalizedName = "MANAGER",
                      },
                      new IdentityRole
                      {
                          Name = "User",
                          NormalizedName = "USER",
                      }
                      );
                context.SaveChanges();
            }
        }
    }
}
