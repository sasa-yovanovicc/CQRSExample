using EmployeeTimeTracking.Models;
using MediatR;

namespace EmployeeTimeTracking.Commands
{
    /// <summary>
    /// Update WorkInterval Command
    /// <description>
    /// Update WorkInterval object
    /// </description>
    /// </summary>
    public class UpdateWorkIntervalCommand : IRequest<bool>
    {
        public required WorkIntervalUpdateRequestModel WorkInterval { get; set; }
    }
}
