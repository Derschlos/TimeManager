//using ShiftLoger.Models;
using System.Globalization;
using TimeManagerClassLibrary.Models;

namespace TimeManagerClassLibrary.Interfaces
{
    public interface ILogFactory
    {
        TimeSpan CalculateLogTime(LogModel Log);
        LogModel CreateNewLog(string UserId, string Comment);
        LogModel CreateNewLog(DateTime date, string UserId, string Comment);
        List<LogModel> CreateLogsSpaningDays(LogModel Log);
        bool IsLogDatesMatch(LogModel Log);
        int CalculateWeekOfYear(DateTime date);

    }
}
