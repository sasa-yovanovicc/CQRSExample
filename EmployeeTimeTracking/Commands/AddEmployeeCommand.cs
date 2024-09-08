using EmployeeTimeTracking.Models;
using MediatR;

namespace EmployeeTimeTracking.Commands
{
    /// <summary>
    /// Add Employee Command
    /// <description>
    /// Add Employee object 
    /// </description>
    /// </summary>
    public class AddEmployeeCommand : IRequest<bool>
    {
        public required EmployeeRequestModel Employee { get; set; }
    }

}
