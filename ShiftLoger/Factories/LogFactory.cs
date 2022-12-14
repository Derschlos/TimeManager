using ShiftLoger.Interfaces;
//using ShiftLoger.Models;
using TimeManagerClassLibrary.Models;

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

        public List<LogModel> CreateLogsSpaningDays(LogModel Log)
        {
            if (!Log.EndTime.HasValue)
            {
                return null;
            }
            var daysBetweenStartAndEnd = (Log.EndTime.Value.Day - Log.StartTime.Day);
            List<LogModel> LogsBetweenDates = Enumerable.Range(1, (int)daysBetweenStartAndEnd).Select(index =>
                new LogModel(Log.UserId)
                {
                    UserId = Log.UserId,
                    StartTime = Log.StartTime.
                                    Date.
                                    AddDays(index),
                    EndTime = Log.StartTime.
                                    Date.
                                    AddDays(index).
                                    AddHours(24).
                                    Subtract(new TimeSpan(1)),
                    Comment = Log.Comment
                }
                ).ToList();
            foreach (var logs in LogsBetweenDates)
            {
                logs.LogTime = CalculateLogTime(logs);
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

        public bool IsLogDatesMatch(LogModel Log)
        {
            return Log.EndTime.Value.Date == Log.StartTime.Date;
        }
    }
}
