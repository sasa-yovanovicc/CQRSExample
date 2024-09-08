using EmployeeTimeTracking.Models;
using MediatR;
using System.Collections.Generic;

namespace EmployeeTimeTracking.Queries
{
    public class GetWorkIntervalsByEmployeeIdQuery : IRequest<List<WorkIntervalResponseModel>>
    {
        public int EmployeeId { get; set; }
    }
}
