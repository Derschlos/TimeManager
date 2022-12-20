using ShiftLoger.Contexts;
using ShiftLoger.Interfaces;
using System.Globalization;
//using ShiftLoger.Models;
using TimeManagerClassLibrary.Models;

namespace ShiftLoger.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly ShiftLogerContext _context;
        private readonly IQueryable<LogModel> _log;
        private readonly CultureInfo _cultureInfo;
        private readonly Calendar _calendar;

        public LogRepository(ShiftLogerContext context)
        {
            _context = context;
            _log = from l in _context.LogModel select l;
            _cultureInfo = new CultureInfo("de-DE");
            _calendar = _cultureInfo.Calendar;
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


        public async Task<ICollection<LogModel>> GetCurrentLogsAsync(string UserId)
        {
            var now = DateTime.Now;
            var currentWeek = CalculateWeekOfYear(now);
            return await Task.FromResult(new List<LogModel>(_log.
                Where(log => log.UserId.Equals(UserId)).
                Where(log => log.StartTime.Year == now.Year).
                Where(Log => currentWeek.Equals(Log.WeekOfYear)).
                OrderByDescending(log => log.StartTime)));
        }

        public LogModel GetLogById(string id)
        {
            throw new NotImplementedException();
        }

        public ICollection<LogModel> GetLogsByMonth(string UserId, int Month, int Year)
        {
            return new List<LogModel>(_log.
                Where(log => log.UserId.Equals(UserId)).
                Where(log => log.StartTime.Year == Year).
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
        public int CalculateWeekOfYear(DateTime date)
        {
            return _calendar.GetWeekOfYear( 
                date, 
                _cultureInfo.DateTimeFormat.CalendarWeekRule,
                _cultureInfo.DateTimeFormat.FirstDayOfWeek
                );
        }
    }
}
