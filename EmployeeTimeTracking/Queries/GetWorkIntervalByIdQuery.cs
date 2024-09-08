using MediatR;
using EmployeeTimeTracking.Data.Entities;

namespace EmployeeTimeTracking.Queries
{
    public class GetWorkIntervalByIdQuery : IRequest<WorkInterval>
    {
        public int Id { get; set; }
    }
}
