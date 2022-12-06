using Microsoft.AspNetCore.Mvc;
using ShiftLoger.Contexts;
using ShiftLoger.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Loger.Controllers
{
    [Route("ShiftLoger/[controller]")]
    [ApiController]
    public class LogerController : ControllerBase
    {
        private readonly ShiftLogerContext _context;

        public LogerController(ShiftLogerContext context)
        {
            _context = context;
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
            var logs = from l in _context.LogModel select l;
            var latestLogs = logs.
                Where(log => log.UserId.Equals(UserId)).
                OrderByDescending(log => log.StartTime).
                Take(3);
            return latestLogs;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public LogModel Post([FromBody] int Id,string UserId, string? comment)
        {
            var logs = from l in _context.LogModel select l;
            var now = DateTime.Now;
            //var latestLogs = logs.
            //    Where(log => log.UserId.Equals(UserId)).
            //    OrderBy(log => log.StartDate).
            //    Take(3);
            var newLog = new LogModel
            {
                Id = Guid.NewGuid().ToString(),
                UserId = UserId,
                StartTime = now,
                Comment = comment
            };
            _context.Add(newLog);
            //_context.Update(newLog);
            _context.SaveChanges();

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
