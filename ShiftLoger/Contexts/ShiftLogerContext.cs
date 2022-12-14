using Microsoft.EntityFrameworkCore;

namespace ShiftLoger.Contexts
{
    public class ShiftLogerContext : DbContext

    {
        public ShiftLogerContext(DbContextOptions<ShiftLogerContext> options)
           : base(options)
        {
        }

        public DbSet<TimeManagerClassLibrary.Models.LogModel> LogModel { get; set; } = default!;
    }
}
