using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManagerClassLibrary.Interfaces;
using TimeManagerClassLibrary.Models;

namespace TimeManagerClassLibrary.Factories
{
    internal class LogedDaysFactory : ILogedDaysFactory
    {


        public LogedDaysDTOModel CreateDTOModel(LogedDaysModel LogedDay, ICollection<LogModel> Logs)
        {
            return new LogedDaysDTOModel() {
                Logs = Logs,
                Date = LogedDay.Date,
                WeekOfYear = LogedDay.WeekOfYear,
                TimeLoged = LogedDay.TimeLoged,
            };
        }


        // Creates a Collection of LogedDaysModels out of a collection of Logs
        // Doesn't save the Models to DB
        public ICollection<LogedDaysModel> CreateLogedDays(ICollection<LogModel> Logs)
        {
            ICollection<LogedDaysModel> logedDaysList = new List<LogedDaysModel>();
            var logedDaysDict = new Dictionary<DateTime, List<LogModel>>();
            foreach (var log in Logs)
            {
                var logDate = log.StartTime.Date;
                if (logedDaysDict.ContainsKey(logDate))
                {
                    logedDaysDict[logDate].Add(log);
                }
                else
                {
                    logedDaysDict.Add(logDate, new List<LogModel>() { log});
                }               
            }
            foreach (var date in logedDaysDict)
            {
                logedDaysList.Add(new LogedDaysModel(date.Value));
            }
            return logedDaysList;
        }
    }
}
