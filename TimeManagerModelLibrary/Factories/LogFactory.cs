﻿using System.Globalization;
using TimeManagerClassLibrary.Interfaces;
//using ShiftLoger.Models;
using TimeManagerClassLibrary.Models;

namespace TimeManagerClassLibrary.Factories
{
    public class LogFactory : ILogFactory
    {
        private readonly CultureInfo _cultureInfo;
        private readonly Calendar _calendar;
        public LogFactory()
        {
            // _cultureInfo = new CultureInfo(localisation);
            _cultureInfo = new CultureInfo("de-DE");
            _calendar = _cultureInfo.Calendar;
        }

        public TimeSpan CalculateLogTime(LogModel Log)
        {
            if (Log.EndTime == null)
            {
                return TimeSpan.Zero;
            }
            var LogTime = Log.EndTime - Log.StartTime;
            return LogTime.Value;
        }

        public int CalculateWeekOfYear(DateTime date)
        {
            return _calendar.GetWeekOfYear(
                date,
                _cultureInfo.DateTimeFormat.CalendarWeekRule,
                _cultureInfo.DateTimeFormat.FirstDayOfWeek
                );
        }

        public List<LogModel> CreateLogsSpaningDays(LogModel Log)
        {
            if (!Log.EndTime.HasValue)
            {
                return null;
            }
            var daysBetweenStartAndEnd = Log.EndTime.Value.Day - Log.StartTime.Day;
            List<LogModel> LogsBetweenDates = Enumerable.Range(1, daysBetweenStartAndEnd).Select(index =>
                new LogModel(Log.UserId)
                {
                    UserId = Log.UserId,
                    StartTime = Log.StartTime.
                                    Date.
                                    AddDays(index),
                    EndTime = Log.StartTime.
                                    Date.
                                    AddDays(index).
                                    AddHours(24).
                                    Subtract(new TimeSpan(1)),
                    Comment = Log.Comment,
                    WeekOfYear = CalculateWeekOfYear(Log.StartTime.
                                    Date.
                                    AddDays(index))
                }
                ).ToList();
            foreach (var logs in LogsBetweenDates)
            {
                logs.LogTime = CalculateLogTime(logs);
            }
            return LogsBetweenDates;
        }

        public LogModel CreateNewLog(string UserId, string Comment)
        {
            return new LogModel(UserId)
            {
                Id = Guid.NewGuid().ToString(),
                UserId = UserId,
                StartTime = DateTime.Now,
                Comment = Comment,
                WeekOfYear = CalculateWeekOfYear(DateTime.Now)
            };
        }

        public LogModel CreateNewLog(DateTime date, string UserId, string Comment)
            // overload to set StartTime to date
        {
            return new LogModel(UserId)
            {
                Id = Guid.NewGuid().ToString(),
                UserId = UserId,
                StartTime = date,
                Comment = Comment,
                WeekOfYear = CalculateWeekOfYear(date)
            };
        }

        public bool IsLogDatesMatch(LogModel Log)
        {
            return Log.EndTime.Value.Date == Log.StartTime.Date;
        }
    }
}