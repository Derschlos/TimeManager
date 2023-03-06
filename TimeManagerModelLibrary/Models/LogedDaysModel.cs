
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagerClassLibrary.Models
{
    [NotMapped]
    public class LogedDaysModel
    {
        public ICollection<LogModel> Logs { get; set; }
        public DateTime Date { get; set; } 
        public TimeSpan TimeLoged { get; set; } 
        public int WeekOfYear { get; set; }
        public LogedDaysModel(ICollection<LogModel> Logs)
        {
            this.Logs = Logs;
            Date = Logs.FirstOrDefault().StartTime;
            TimeLoged = new TimeSpan(Logs.Where(l =>l.LogTime != null).Sum(l => l.LogTime.Value.Ticks));
            WeekOfYear = (int)Logs.FirstOrDefault().WeekOfYear;
        }
    }
}
