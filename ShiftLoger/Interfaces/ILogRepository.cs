using ShiftLoger.Models;

namespace ShiftLoger.Interfaces
{
    public interface ILogRepository
    {
        ICollection<LogModel> GetLastThreeLogs(string UserId);
        LogModel GetLastLog(string UserId);

        LogModel GetLogById(string id);
        LogModel UpdateLog(string id);
        List<LogModel> AddUsers(List<LogModel> Log);

    }
}
