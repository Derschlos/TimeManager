using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagerClassLibrary.Interfaces
{
    public interface IWorkFactories
    {
        ILogFactory Log { get; }
        ILogedDaysFactory LogedDays { get; }
    }
}
