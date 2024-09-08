using EmployeeTimeTracking.Validators;
using System.ComponentModel.DataAnnotations;

namespace EmployeeTimeTracking.Data.Entities
{
    public class WorkInterval
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Employee ID is required.")]
        public int EmployeeId { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Invalid DateTime format.")]
        public DateTime? Start { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Invalid DateTime format.")]
        [DateGreaterThan("Start", ErrorMessage = "End DateTime should be after Start DateTime.")]
        public DateTime? End { get; set; }

        // Navigation property
        public required Employee Employee { get; set; }

        public TimeSpan GetIntervalDuration()
        {
            // Only calculate duration if both Start and End are set
            TimeSpan TimeSpan = TimeSpan.Zero;
            if (Start != DateTime.MinValue && End != DateTime.MinValue)
            {
                return (TimeSpan)(End - Start);
            }
            return TimeSpan;
        }
    }
}
