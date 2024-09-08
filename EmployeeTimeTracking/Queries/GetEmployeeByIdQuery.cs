using EmployeeTimeTracking.Models;
using MediatR;

namespace EmployeeTimeTracking.Queries
{
    public class GetEmployeeByIdQuery : IRequest<EmployeeResponseModel>
    {
        public int Id { get; set; }
    }
}
