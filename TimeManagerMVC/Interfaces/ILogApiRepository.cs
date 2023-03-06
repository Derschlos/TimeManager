using TimeManagerClassLibrary.Models;

namespace TimeManagerMVC.Interfaces
{
    public interface ILogApiRepository
    {
        Task<ICollection<LogedDaysModel>> GetLogsAsync(string UserId);
        Task<ICollection<LogedDaysModel>> GetLogsByMonthAsync(string UserId, int Month, int Year);
        Task<ICollection<LogedDaysModel>> ParseResponseAsync(HttpResponseMessage Response);
  

    }
}
