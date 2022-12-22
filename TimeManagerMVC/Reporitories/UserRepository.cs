using TimeManagerMVC.Areas.Identity.Data;
using TimeManagerMVC.Data;
using TimeManagerMVC.Interfaces;

namespace TimeManagerMVC.Reporitories
{
    public class UserRepository : IUserRepository
    {
        private readonly TimeManagerUserContext _context;

        public UserRepository(TimeManagerUserContext context)
        {
            _context = context;
        }

        public ICollection<TimeManagerUser> GetTimeManagerUsers()
        {
            return _context.Users.ToList();
        }

        public TimeManagerUser GetUserById(string Id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == Id);
        }

        public TimeManagerUser GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == username);
        }

        public TimeManagerUser UpdateUser(TimeManagerUser user)
        {
            _context.Update(user);
            _context.SaveChanges();
            return user;
        }
    }
}
