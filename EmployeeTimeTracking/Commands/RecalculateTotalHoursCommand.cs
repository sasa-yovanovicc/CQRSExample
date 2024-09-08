using MediatR;

namespace EmployeeTimeTracking.Commands
{
    /// <summary>
    /// Recalculate Total Hours Command
    /// </summary>
    public class RecalculateTotalHoursCommand : IRequest<bool>
    {
    }
}
