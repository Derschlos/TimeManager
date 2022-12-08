using System.ComponentModel.DataAnnotations;

namespace ShiftLoger.Models
{
    public class LogModel
    {
        public LogModel(string UserId)
        {
            Id = Guid.NewGuid().ToString();
            this.UserId = UserId;
        }

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
    }
}
