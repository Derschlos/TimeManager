using ShiftLoger.Contexts;
using ShiftLoger.Interfaces;
using ShiftLoger.Models;

namespace ShiftLoger.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly ShiftLogerContext _context;
        private readonly IQueryable<LogModel> _log;

        public LogRepository(ShiftLogerContext context)
        {
            _context = context;
            _log = from l in _context.LogModel select l;
        }

        public List<LogModel> AddUsers(List<LogModel> Logs)
        {
            _context.AddRange(Logs);
            _context.SaveChanges();
            return Logs;
        }


        public LogModel GetLastLog(string UserId)
        {
            return _log.
                Where(log => log.UserId.Equals(UserId)).
                OrderByDescending(log => log.StartTime).
                Max();
        }


        public ICollection<LogModel> GetLastThreeLogs(string UserId)
        {
            return new List<LogModel>(_log.
                Where(log => log.UserId.Equals(UserId)).
                OrderByDescending(log => log.StartTime).
                Take(3));
        }

        public LogModel GetLogById(string id)
        {
            throw new NotImplementedException();
        }

        public LogModel UpdateLog(string id)
        {
            throw new NotImplementedException();
        }
    }
}
