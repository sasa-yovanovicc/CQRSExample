using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeTimeTracking.Data.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name can be up to 50 characters long.")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name can be up to 50 characters long.")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Job position is required.")]
        [StringLength(100, ErrorMessage = "Job position can be up to 100 characters long.")]
        public required string Position { get; set; }

        [Required(ErrorMessage = "Hire date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        public DateTime HireDate { get; set; }
        
        [Column(TypeName = "decimal(7,2)")]
        public decimal? TotalHours { get; set; }
        public ICollection<WorkInterval> WorkIntervals { get; set; }

        // Recalculate TotalHours if needed
        /*
        public double GetTotalWorkedHours()
        {
            double totalHours = 0;
            foreach (var interval in WorkIntervals)
            {
                var duration = interval.GetIntervalDuration();
                if (duration.HasValue)
                {
                    totalHours += duration.Value.TotalHours;
                }
            }
            return totalHours;
        }
        */
        // Recalculate TotalHours if needed
        public void RecalculateTotalHours()
        {
            TotalHours = (decimal?)WorkIntervals
                .Where(wi => wi.Start != DateTime.MinValue && wi.End != DateTime.MinValue)
                .Sum(wi => wi.GetIntervalDuration().TotalHours);
        }
    }

}
