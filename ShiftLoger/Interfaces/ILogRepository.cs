using ShiftLoger.Models;

namespace ShiftLoger.Interfaces
{
    public interface ILogRepository
    {
        ICollection<LogModel> GetLastThreeLogs(string UserId);
        LogModel GetLastLog(string UserId);

        LogModel GetLogById(string id);
        LogModel UpdateLog(LogModel Log);
        List<LogModel> AddLogs(List<LogModel> Log);
        ICollection<LogModel> GetLogsByMonth(string UserId,int Month);
        ValueTask<LogModel> FindAsync(string Id);
        void RemoveLog(LogModel Log);

    }
}
