namespace EmployeeTimeTracking.Models
{
    public class EmployeeRequestModel
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Position { get; set; }
        public DateTime HireDate { get; set; }
    }

}
