//using ShiftLoger.Models;
using TimeManagerClassLibrary.Models;

namespace ShiftLoger.Interfaces
{
    public interface ILogFactory
    {
        TimeSpan CalculateLogTime(LogModel Log);
        LogModel CreateNewLog(string UserId, string Comment);
        List<LogModel> CreateLogsSpaningDays(LogModel Log);
        bool IsLogDatesMatch (LogModel Log);
        
    }
}
