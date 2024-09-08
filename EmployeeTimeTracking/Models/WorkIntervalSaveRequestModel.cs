namespace EmployeeTimeTracking.Models
{
    public class WorkIntervalSaveRequestModel
    {
        public int EmployeeId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}