using System.ComponentModel.DataAnnotations;

namespace ShiftLoger.Models
{
    public class LogModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartTime { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndTime { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan? LogTime { get; set; }

        [DataType(DataType.Text)]
        public string? Comment { get; set; }
        
        public TimeSpan calculateLogTime()
        {
            if (EndTime == null)
            {
                return TimeSpan.Zero;
            }
            LogTime = EndTime - StartTime;
            return LogTime.Value;
        }

        //public LogModel(string userId, DateTime start,DateTime? end, string? comment)
        //{
        //    Id = Guid.NewGuid().ToString();
        //    StartTime = start;
        //    EndTime = end;
        //    Comment = comment;
            
        //}

    }
}
