
using Microsoft.EntityFrameworkCore;
using ShiftLoger.Contexts;
using ShiftLoger.Interfaces;
using ShiftLoger.Models;

namespace ShiftLoger.Data
{
    public class SeedData
    {
        public static void InitializeLogData(IServiceProvider serviceProvider, ILogFactory logFactory)
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
                    new LogModel("a")
                    {
                        StartTime = now.Subtract(oneDay * 3),
                        EndTime = now.Subtract(oneDay * 3).AddHours(5).AddMinutes(17),
                        Comment = $"This is comment {++commentCounter}"
                    },
                    new LogModel("a")
                    {
                        StartTime = now.Subtract(oneDay * 2),
                        EndTime = now.Subtract(oneDay * 2).AddHours(3).AddMinutes(30),
                        Comment = $"This is comment {++commentCounter}"
                    },
                    new LogModel("a")
                    {
                        StartTime = now.Subtract(oneDay * 1),
                        EndTime = now.Subtract(oneDay * 1).AddHours(8),
                        Comment = $"This is comment {++commentCounter}"
                    },
                    new LogModel("b")
                    {
                        StartTime = now.Subtract(oneDay * 7),
                        EndTime = now.Subtract(oneDay * 7).AddHours(5),
                        Comment = $"This is the only complete for b"
                    },
                    new LogModel("a")
                    {
                        StartTime = now.Subtract(oneDay * 1).AddHours(1),
                        Comment = $"This is comment {++commentCounter}"
                    },
                    new LogModel("b")
                    {
                        StartTime = now.Subtract(oneDay * 2),
                        Comment = $"This is the open post for b"
                    },
                };
                foreach (var log in ModelsToAdd)
                {
                    if (log.EndTime != null)
                    {
                        log.LogTime = logFactory.CalculateLogTime(log);
                    }
                }
                context.LogModel.AddRange(ModelsToAdd);
                
                context.SaveChanges();
            }
        }

    }
}
