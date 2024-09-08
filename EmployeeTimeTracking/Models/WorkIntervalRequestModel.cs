namespace EmployeeTimeTracking.Models
{
    public class WorkIntervalRequestModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Position { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public WorkIntervalRequestModel()
        {
            Start = DateTime.Now;
            End = DateTime.Now.AddHours(1); 
        }
    }


}
