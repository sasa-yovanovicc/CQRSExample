using EmployeeTimeTracking.Models;
using MediatR;

namespace EmployeeTimeTracking.Commands
{
    /// <summary>
    /// Update Employee Command
    /// <description>
    /// Update Employee object
    /// </description>
    /// </summary>
    public class UpdateEmployeeCommand : IRequest<bool>
    {
        public EmployeeRequestModel? Employee { get; set; }
    }

}
