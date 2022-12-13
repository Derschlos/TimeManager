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

        public List<LogModel> AddLogs(List<LogModel> Logs)
        {
            _context.AddRange(Logs);
            _context.SaveChanges();
            return Logs;
        }

        public async ValueTask<LogModel> FindAsync(string Id)
        {
            return await _context.LogModel.FindAsync(Id);
        }

        public LogModel GetLastLog(string UserId)
        {
            return _log.
                Where(log => log.UserId.Equals(UserId)).
                OrderByDescending(log => log.StartTime).FirstOrDefault();
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

        public ICollection<LogModel> GetLogsByMonth(string UserId, int Month)
        {
            return new List<LogModel>(_log.
                Where(log => log.UserId.Equals(UserId)).
                Where(log => log.StartTime.Month == Month));
        }

        public void RemoveLog(LogModel Log)
        {
            _context.Remove(Log);
        }

        public LogModel UpdateLog(LogModel Log)
        {
            _context.Update(Log);
            _context.SaveChanges();
            return Log;
        }
    }
}
