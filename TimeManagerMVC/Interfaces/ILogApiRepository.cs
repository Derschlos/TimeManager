using TimeManagerClassLibrary.Models;

namespace TimeManagerMVC.Interfaces
{
    public interface ILogApiRepository
    {
        Task<ICollection<LogModel>> GetLogsAsync(string UserId);
        Task<ICollection<LogModel>> GetLogsByMonthAsync(string UserId, int Month, int Year);
        Task<ICollection<LogModel>> ParseResponseAsync(HttpResponseMessage Response);
    }
}
