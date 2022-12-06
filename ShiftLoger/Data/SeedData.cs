
using Microsoft.EntityFrameworkCore;
using ShiftLoger.Contexts;
using ShiftLoger.Models;

namespace ShiftLoger.Data
{
    public class SeedData
    {
        public static void InitializeLogData(IServiceProvider serviceProvider)
        {
            using (var context = new ShiftLogerContext(
                serviceProvider.GetRequiredService<DbContextOptions<ShiftLogerContext>>()))
            {
                if (context.LogModel.Any())
                {
                    return;
                }
                var oneDay = DateTime.Now.AddDays(1) - DateTime.Now;
                var now = DateTime.Now;
                var commentCounter = 0;
                var ModelsToAdd = new List<LogModel>() {
                    new LogModel
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = "a",
                        StartTime = now.Subtract(oneDay * 3),
                        EndTime = now.Subtract(oneDay * 3).AddHours(5).AddMinutes(17),
                        Comment = $"This is comment {++commentCounter}"
                    },
                    new LogModel
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = "a",
                        StartTime = now.Subtract(oneDay * 2),
                        EndTime = now.Subtract(oneDay * 2).AddHours(3).AddMinutes(30),
                        Comment = $"This is comment {++commentCounter}"
                    },
                    new LogModel
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = "a",
                        StartTime = now.Subtract(oneDay * 1),
                        EndTime = now.Subtract(oneDay * 1).AddHours(9),
                        Comment = $"This is comment {++commentCounter}"
                    },
                    new LogModel
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = "b",
                        StartTime = now.Subtract(oneDay * 3),
                        EndTime = now.Subtract(oneDay * 3).AddHours(5),
                        Comment = $"This is the only comment for b"
                    },
                    new LogModel
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = "a",
                        StartTime = now,
                        Comment = $"This is comment {++commentCounter}"
                    }
                };
                foreach (var log in ModelsToAdd)
                {
                    if (log.EndTime != null)
                    {
                        log.LogTime = log.EndTime - log.StartTime;
                    }
                }
                context.LogModel.AddRange(ModelsToAdd);
                
                context.SaveChanges();
            }
        }

    }
}
