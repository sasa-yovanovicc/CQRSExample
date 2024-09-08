using EmployeeTimeTracking.Models;
using MediatR;
using System.Collections.Generic;

namespace EmployeeTimeTracking.Queries
{
    public class GetAllEmployeesQuery : IRequest<List<EmployeeResponseModel>>
    {
    }

}
