using System.ComponentModel.DataAnnotations;

namespace ShiftLoger.Models
{
    public class LogModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartTime { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndTime { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan? LogTime { get; set; }

    }
}
