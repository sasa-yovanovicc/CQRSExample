using AutoMapper;
using EmployeeTimeTracking.Data;
using EmployeeTimeTracking.Models;
using EmployeeTimeTracking.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Handlers
{
    public class GetWorkIntervalQueryHandler : IRequestHandler<GetWorkIntervalQuery, WorkIntervalResponseModel>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetWorkIntervalQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Handler for GetWorkIntervalQuery
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<WorkIntervalResponseModel?> Handle(GetWorkIntervalQuery request, CancellationToken cancellationToken)
        {
            var workInterval = await _context.WorkIntervals
                .Include(w => w.Employee)
                .FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken);

            return workInterval != null ? _mapper.Map<WorkIntervalResponseModel>(workInterval) : null;
        }
    }
}
