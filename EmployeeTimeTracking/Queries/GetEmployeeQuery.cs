using EmployeeTimeTracking.Models;
using MediatR;
using System.Collections.Generic;

namespace EmployeeTimeTracking.Queries
{
    public class GetEmployeeQuery : IRequest<EmployeeResponseModel>
    {
        public int Id { get; set; }
    }

}
