using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManagerClassLibrary.Interfaces;

namespace TimeManagerClassLibrary.Factories
{
    public class WorkFactories : IWorkFactories
    {
        public ILogFactory Log {get; set;}

        public ILogedDaysFactory LogedDays { get; set; }
        public WorkFactories(ILogFactory Log, ILogedDaysFactory LogedDays)
        {
            this.Log = Log;
            this.LogedDays = LogedDays;
        }
    }
}
