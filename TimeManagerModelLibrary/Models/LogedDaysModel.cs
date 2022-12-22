
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagerClassLibrary.Models
{
    internal class LogedDaysModel
    {
        public ICollection<string> Logs { get; set; }
        public DateTime Date { get; set; } 
        public TimeSpan TimeLoged { get; set; } 
        public int WeekOfYear { get; set; } 
        public LogedDaysModel(ICollection<LogModel> Logs)
        {
            this.Logs = Logs.Select(l => l.Id).ToList();
            Date = Logs.FirstOrDefault().StartTime;
            TimeLoged = new TimeSpan(Logs.Sum(l => l.LogTime.Value.Ticks));
            WeekOfYear = (int)Logs.FirstOrDefault().WeekOfYear;
        }
    }
}
