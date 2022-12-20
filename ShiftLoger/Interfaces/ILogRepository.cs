//using ShiftLoger.Models;
using TimeManagerClassLibrary.Models;

namespace ShiftLoger.Interfaces
{
    public interface ILogRepository
    {
        Task<ICollection<LogModel>> GetCurrentLogsAsync(string UserId);
        LogModel GetLastLog(string UserId);

        LogModel GetLogById(string id);
        LogModel UpdateLog(LogModel Log);
        List<LogModel> AddLogs(List<LogModel> Log);
        ICollection<LogModel> GetLogsByMonth(string UserId,int Month, int Year);
        ValueTask<LogModel> FindAsync(string Id);
        void RemoveLog(LogModel Log);

    }
}
