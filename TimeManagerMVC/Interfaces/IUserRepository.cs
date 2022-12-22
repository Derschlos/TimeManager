using TimeManagerMVC.Areas.Identity.Data;

namespace TimeManagerMVC.Interfaces
{
    public interface IUserRepository
    {
        ICollection<TimeManagerUser> GetTimeManagerUsers();
        TimeManagerUser GetUserById(string Id);
        TimeManagerUser UpdateUser(TimeManagerUser user);
        TimeManagerUser GetUserByUsername(string username);
    }
}
