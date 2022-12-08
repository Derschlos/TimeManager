using ShiftLoger.Models;

namespace ShiftLoger.Interfaces
{
    public interface ILogFactory
    {
        TimeSpan CalculateLogTime(LogModel Log);
        LogModel CreateNewLog(string UserId, string Comment);
        List<LogModel> CreateLogsSpaningDays(string UserId, DateTime Start, DateTime End, string Comment);
    }
}
