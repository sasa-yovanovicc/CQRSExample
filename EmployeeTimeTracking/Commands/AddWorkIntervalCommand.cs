using EmployeeTimeTracking.Models;
using MediatR;

namespace EmployeeTimeTracking.Commands
{
    /// <summary>
    /// Add Work Interval Command
    /// <description>
    /// Add WorkInterval object 
    /// </description>
    /// </summary>
    public class AddWorkIntervalCommand : IRequest<bool>
    {
        public WorkIntervalSaveRequestModel? WorkInterval { get; set; }
    }

}
