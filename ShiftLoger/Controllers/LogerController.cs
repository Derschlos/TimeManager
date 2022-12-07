using Microsoft.AspNetCore.Mvc;
using ShiftLoger.Contexts;
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
        private readonly UnitOfWork _unitOfWork;

        //private readonly ShiftLogerContext _context;

        //public LogerController(ShiftLogerContext context)
        //{
        //    _context = context;
        //}

        public LogerController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/<ValuesController>
        //[HttpGet]
        //public IEnumerable<LogModel> Get()//IEnumerable<string> Get()
        //{
        //    var logs = from l in _context.LogModel select l;
        //    var latestLogs = logs.
        //        Where(log => log.UserId.Equals("ab")).
        //        OrderByDescending(log => log.StartTime).
        //        Take(3);
        //    return latestLogs;
        //}

        // GET api/<ValuesController>/5
        [HttpGet("{UserId}")]
        public IEnumerable<LogModel> Get(string UserId)
        {
            return _unitOfWork.Log.GetLastThreeLogs(UserId);
        }


        // POST api/<ValuesController>
        //[FromBody] int Id
        [HttpPost]
        public LogModel Post(string UserId, string? comment)
        {
            var newLog= new LogModel();
            if (string.IsNullOrEmpty(UserId))
            {
                return null;
            }
            var now = DateTime.Now;
            var latestLog = _unitOfWork.Log.GetLastLog(UserId);

            if (latestLog.EndTime != null)
            {
                newLog = new LogModel {
                    UserId = UserId,
                    StartTime = now,
                    Comment = comment };
                _unitOfWork.Log.AddUsers(new List<LogModel> { newLog });
                return newLog;
            }

            var daysBetweenStartAndEnd = (now - latestLog.StartTime).TotalDays;
            List<LogModel> LogsBetweenDates = Enumerable.Range(1, (int)daysBetweenStartAndEnd).Select(index =>
                new LogModel {
                    UserId = UserId,
                    StartTime = latestLog.StartTime.Date.AddDays(index),
                    EndTime = latestLog.StartTime.Date.AddDays(index).AddHours(24),
                    LogTime = TimeSpan.FromDays(1),
                    Comment = comment
                }
                ).ToList();

            if (LogsBetweenDates.Any())
            {
                latestLog.EndTime = latestLog.StartTime.Date.AddHours(24);
                latestLog.calculateLogTime();
                _unitOfWork.Log.UpdateLog(latestLog.Id);

                LogsBetweenDates.LastOrDefault().EndTime = now;
                LogsBetweenDates.LastOrDefault().calculateLogTime();
                newLog = LogsBetweenDates.LastOrDefault();
            }
            else
            {
                newLog = latestLog;
                newLog.EndTime = now;
                newLog.calculateLogTime();

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
        //public LogModel createNewModel(DateTime start, string userId, string? comment)
        //{
        //    var newLog = new LogModel
        //    {
        //        //Id = Guid.NewGuid().ToString(),
        //        userId = userId,
        //        startTime = start,
        //        comment = comment
        //    };
        //    return newLog;
        //}
    }
}
