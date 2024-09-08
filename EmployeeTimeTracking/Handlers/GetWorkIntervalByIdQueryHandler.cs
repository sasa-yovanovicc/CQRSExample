using EmployeeTimeTracking.Data;
using EmployeeTimeTracking.Data.Entities;
using EmployeeTimeTracking.Queries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Handlers
{
    public class GetWorkIntervalByIdQueryHandler : IRequestHandler<GetWorkIntervalByIdQuery, WorkInterval>
    {
        private readonly ApplicationDbContext _context;

        public GetWorkIntervalByIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Handler for GetWorkIntervalByIdQuery
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<WorkInterval> Handle(GetWorkIntervalByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.WorkIntervals.FindAsync(request.Id);
        }
    }
}
