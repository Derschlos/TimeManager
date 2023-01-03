using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManagerClassLibrary.Models;

namespace TimeManagerClassLibrary.Interfaces
{
    public interface ILogedDaysFactory
    {
        ICollection<LogedDaysModel> CreateLogedDays(ICollection<LogModel> Logs);
        //LogedDaysDTOModel CreateDTOModel(LogedDaysModel LogedDay, ICollection<LogModel> Logs);
    }
}
