using Microsoft.AspNetCore.Mvc;
using ShiftLoger.Contexts;
using ShiftLoger.Interfaces;
using ShiftLoger.Models;
using ShiftLoger.Repositories;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Loger.Controllers
{
    [Route("ShiftLoger/[controller]")]
    [ApiController]
    public class LogerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogFactory _logFactory;


        //private readonly ShiftLogerContext _context;

        //public LogerController(ShiftLogerContext context)
        //{
        //    _context = context;
        //}

        public LogerController(IUnitOfWork unitOfWork, ILogFactory logFactory)
        {
            _unitOfWork = unitOfWork;
            _logFactory = logFactory;
        }

        //GET: api/<ValuesController>/587-342/Month/12
        // GetLogs for a given Month
        [HttpGet("{UserId}/Month/{Month}")]
        public IEnumerable<LogModel> Get(string UserId, int Month)
        {
            
            return _unitOfWork.Log.GetLogsByMonth(UserId, Month);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{UserId}")]
        public IEnumerable<LogModel> Get(string UserId)
        {
            return _unitOfWork.Log.GetLastThreeLogs(UserId);
        }


        // POST api/<ValuesController>
        //[FromBody] int Id
        // Checks if a Log without EndTime exists, then either creates a new Log or
        // fills the Db with Logs till the EndDate is reached.
        // You can only have logs from Hour 0 to 24 of any given Day

        [HttpPost]
        public LogModel Post(string UserId, string? comment)
        {
            var newLog= new LogModel(UserId);
            if (string.IsNullOrEmpty(UserId))
            {
                return null;
            }
            var now = DateTime.Now;
            var latestLog = _unitOfWork.Log.GetLastLog(UserId);         

            if (latestLog == null || latestLog.EndTime != null)
            {
                newLog = _logFactory.CreateNewLog(UserId, comment);
                _unitOfWork.Log.AddLogs(new List<LogModel> { newLog });
                return newLog;
            }

            List<LogModel> LogsBetweenDates = _logFactory.
                CreateLogsSpaningDays(UserId,latestLog.StartTime, now, comment);

            if (LogsBetweenDates.Any())
            {
                latestLog.EndTime = latestLog.
                    StartTime.
                    Date.
                    AddHours(24).
                    Subtract(new TimeSpan(1));

                latestLog.LogTime = _logFactory.CalculateLogTime(latestLog);
                _unitOfWork.Log.UpdateLog(latestLog);

                if (LogsBetweenDates.FirstOrDefault().StartTime.Date == latestLog.StartTime.Date)
                {
                    LogsBetweenDates.RemoveAt(0);
                }

                newLog = LogsBetweenDates.Last();
                newLog.EndTime = now;
                newLog.LogTime = _logFactory.CalculateLogTime(newLog);
                _unitOfWork.Log.AddLogs(LogsBetweenDates);
            }
            else
            {
                
                latestLog.EndTime = now;
                latestLog.LogTime = _logFactory.CalculateLogTime(latestLog);
                _unitOfWork.Log.UpdateLog(latestLog);
                newLog = latestLog;

            }
            return newLog;
        }


        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
