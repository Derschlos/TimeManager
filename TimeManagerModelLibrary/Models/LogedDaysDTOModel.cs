using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagerClassLibrary.Models
{
    [NotMapped]
    internal class LogedDaysDTOModel
    {
        public ICollection<LogModel> Logs { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan TimeLoged { get; set; }
        public int WeekOfYear { get; set; }
    }
}
