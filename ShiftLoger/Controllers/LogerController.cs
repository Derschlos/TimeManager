using Microsoft.AspNetCore.Mvc;
using ShiftLoger.Contexts;
using ShiftLoger.Interfaces;
//using ShiftLoger.Models;
using ShiftLoger.Repositories;
using System.Linq;
using TimeManagerClassLibrary.Interfaces;
using TimeManagerClassLibrary.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Loger.Controllers
{
    [Route("ShiftLoger/[controller]")]
    [ApiController]
    public class LogerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogFactory _logFactory;

        public LogerController(IUnitOfWork unitOfWork, ILogFactory logFactory)
        {
            _unitOfWork = unitOfWork;
            _logFactory = logFactory;
        }

        //GET: api/<ValuesController>/587-342/Month/12/Year/2022
        // GetLogs for a given Month
        [HttpGet("{UserId}/Month/{Month}/Year/{Year}")]
        public IEnumerable<LogModel> Get(string UserId, int Month, int Year)
        {

            return _unitOfWork.Log.GetLogsByMonth(UserId, Month, Year);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{UserId}")]
        public async Task<IEnumerable<LogModel>> Get(string UserId)
        {
            return await _unitOfWork.Log.GetCurrentLogsAsync(UserId);
        }


        // POST api/<ValuesController>
        //[FromBody] int Id

        // Checks if a Log without EndTime exists, then either creates a new Log or
        // fills the Db with Logs till the EndDate is reached.
        // You can only have logs from Hour 0 to 24 of any given Day

        [HttpPost]
        public LogModel Post(string UserId, string? comment)
        {
            if (string.IsNullOrEmpty(UserId))
            {
                return null;
            }
            var now = DateTime.Now;
            var latestLog = _unitOfWork.Log.GetLastLog(UserId);
            var newLog = _logFactory.CreateNewLog(latestLog.StartTime, UserId, comment);
            newLog.EndTime = now;

            if (latestLog == null || latestLog.EndTime != null) 
                // no open Logs
            {
                newLog = _logFactory.CreateNewLog(UserId, comment);
                _unitOfWork.Log.AddLogs(new List<LogModel> { newLog });
                return newLog;
            }

            List<LogModel> LogsBetweenDates = _logFactory.
                CreateLogsSpaningDays(newLog);

            if (LogsBetweenDates.Any())
                // log start day != log end day
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
        public async Task<IActionResult> Put(string id, [FromBody] LogModel Log)
        {
            if (id != Log.Id)
            {
                return BadRequest();
            }
            var FoundLogModel = await _unitOfWork.Log.FindAsync(id);
            if (FoundLogModel == null)
            {
                return NotFound();
            }


            if (Log.EndTime.Value.Date == Log.StartTime.Date)
            {
                //return BadRequest("Dates do not match, do a Put with Start-End Dates");
                FoundLogModel.StartTime = Log.StartTime;
                FoundLogModel.EndTime = Log.EndTime.Value;
                FoundLogModel.LogTime = _logFactory.CalculateLogTime(Log);
                FoundLogModel.Comment = Log.Comment;
                FoundLogModel.WeekOfYear = _logFactory.CalculateWeekOfYear(Log.StartTime);
                _unitOfWork.Log.UpdateLog(FoundLogModel);
                return Ok(FoundLogModel);
            }
            //else
            //{

            //    var DatesBetweenStartEnd = _logFactory.CreateLogsSpaningDays(Log);
            //    DatesBetweenStartEnd.RemoveAt(0);
            //    if (DatesBetweenStartEnd.Any())
            //    {
            //        DatesBetweenStartEnd.LastOrDefault().EndTime = Log.EndTime;

            //    }
            //    return Ok(FoundLogModel);
            //}
            return BadRequest();
        }

        // PUT api/<ValuesController>/5/UpdateAndAdd
        [HttpPut("{id}/UpdateAndAdd")]
        public async Task<IActionResult> Put(string id, [FromBody] List<LogModel> Logs)
        {
            var userId = Logs.First().UserId;
            var differentUserId = Logs.Select(x=> x.UserId != userId).FirstOrDefault();

            if (differentUserId != null)
            {
                return BadRequest("The Logs supplied can't be from different UserIds");
            }

            var logToUpdate = Logs.First();
            if (!_logFactory.IsLogDatesMatch(logToUpdate))
            {
                return BadRequest("Start and End Dates of the first Log don't match");
            }

            var FoundLogModel = await _unitOfWork.Log.FindAsync(logToUpdate.Id);
            if (FoundLogModel == null)
            {
                return NotFound();
            }
            _unitOfWork.Log.UpdateLog(logToUpdate);
            Logs.RemoveAt(0);
            List<LogModel> logsToAdd = new List<LogModel>();
            foreach (var log in Logs)
            {
                if (!_logFactory.IsLogDatesMatch(log))
                {
                    return BadRequest("One or more results are longer then one Day");
                }
                var newLog = _logFactory.CreateNewLog(log.StartTime, log.UserId, log.Comment);
                newLog.EndTime = log.EndTime;
                newLog.LogTime = _logFactory.CalculateLogTime(newLog);
                logsToAdd.Add(newLog);
            }
            _unitOfWork.Log.AddLogs(logsToAdd);
            return Ok();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var foundLog = await _unitOfWork.Log.FindAsync(id);
            if (foundLog == null)
            {
                return NotFound();
            }
            _unitOfWork.Log.RemoveLog(foundLog);
            return Ok();
        }
    }
}
