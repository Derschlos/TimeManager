using ShiftLoger.Interfaces;
using ShiftLoger.Models;

namespace ShiftLoger.Factories
{
    public class LogFactory : ILogFactory
    {
        public TimeSpan CalculateLogTime(LogModel Log)
        {
            if (Log.EndTime == null)
            {
                return TimeSpan.Zero;
            }
            var LogTime = Log.EndTime - Log.StartTime;
            return LogTime.Value;
        }

        public List<LogModel> CreateLogsSpaningDays(string UserId,DateTime Start, DateTime End , string Comment)
        {
            var daysBetweenStartAndEnd = (End.Day - Start.Day);
            List<LogModel> LogsBetweenDates = Enumerable.Range(1, (int)daysBetweenStartAndEnd).Select(index =>
                new LogModel(UserId)
                {
                    UserId = UserId,
                    StartTime = Start.
                                    Date.
                                    AddDays(index),
                    EndTime = Start.
                                    Date.
                                    AddDays(index).
                                    AddHours(24).
                                    Subtract(new TimeSpan(1)),
                    Comment = Comment
                }
                ).ToList();
            foreach (var log in LogsBetweenDates)
            {
                log.LogTime = CalculateLogTime(log);
            }
            return LogsBetweenDates;
        }

        public LogModel CreateNewLog(string UserId, string Comment)
        {
            return new LogModel(UserId)
            {
                Id = Guid.NewGuid().ToString(),
                UserId = UserId,
                StartTime = DateTime.Now,
                Comment = Comment
            };
        }
    }
}
