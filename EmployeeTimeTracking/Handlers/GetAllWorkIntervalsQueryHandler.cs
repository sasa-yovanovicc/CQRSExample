using AutoMapper;
using EmployeeTimeTracking.Data;
using EmployeeTimeTracking.Models;
using EmployeeTimeTracking.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTimeTracking.Handlers
{
    public class GetAllWorkIntervalsQueryHandler : IRequestHandler<GetAllWorkIntervalsQuery, List<WorkIntervalResponseModel>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllWorkIntervalsQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Handler for GetAllWorkIntervalsQuery
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<WorkIntervalResponseModel>> Handle(GetAllWorkIntervalsQuery request, CancellationToken cancellationToken)
        {
            var workIntervals = await _context.WorkIntervals
                .Include(w => w.Employee)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<WorkIntervalResponseModel>>(workIntervals);
        }
    }
}
