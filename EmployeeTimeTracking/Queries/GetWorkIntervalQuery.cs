using EmployeeTimeTracking.Models;
using MediatR;

namespace EmployeeTimeTracking.Queries
{
    public class GetWorkIntervalQuery : IRequest<WorkIntervalResponseModel>
    {
        public int Id { get; set; }
    }
}
