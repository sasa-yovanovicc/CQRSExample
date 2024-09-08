using EmployeeTimeTracking.Models;
using MediatR;
using System.Collections.Generic;

namespace EmployeeTimeTracking.Queries
{
    public class GetAllWorkIntervalsQuery : IRequest<List<WorkIntervalResponseModel>>
    {
    }
}
