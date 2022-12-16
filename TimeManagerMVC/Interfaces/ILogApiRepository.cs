using TimeManagerClassLibrary.Models;

namespace TimeManagerMVC.Interfaces
{
    public interface ILogApiRepository
    {
        Task<ICollection<LogModel>> GetLogsAsync(string UserId);
    }
}
