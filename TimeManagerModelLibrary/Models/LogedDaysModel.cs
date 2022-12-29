
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagerClassLibrary.Models
{
    public class LogedDaysModel
    {
        public ICollection<string> LogId { get; set; }
        public DateTime Date { get; set; } 
        public TimeSpan TimeLoged { get; set; } 
        public int WeekOfYear { get; set; } 
        //public LogedDaysModel(ICollection<LogModel> Logs)
        //{
        //    this.LogId = Logs.Select(l => l.Id).ToList();
        //    Date = Logs.FirstOrDefault().StartTime;
        //    TimeLoged = new TimeSpan(Logs.Sum(l => l.LogTime.Value.Ticks));
        //    WeekOfYear = (int)Logs.FirstOrDefault().WeekOfYear;
        //}
    }
}
