namespace EmployeeTimeTracking.Models
{
    public class WorkIntervalResponseModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Position { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public double Duration { get; set; }
    }
}
