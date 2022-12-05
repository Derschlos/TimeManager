using Microsoft.EntityFrameworkCore;

namespace ShiftLoger.Contexts
{
    public class ShiftLogerContext : DbContext

    {
        public ShiftLogerContext(DbContextOptions<ShiftLogerContext> options)
           : base(options)
        {
        }

        public DbSet<ShiftLoger.Models.LogModel> LogModel { get; set; } = default!;
    }
}
