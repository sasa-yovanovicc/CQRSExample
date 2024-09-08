using AutoMapper;
using EmployeeTimeTracking.Data;
using EmployeeTimeTracking.Models;
using EmployeeTimeTracking.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTimeTracking.Handlers
{
    public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, EmployeeResponseModel>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetEmployeeQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Handler for GetEmployeeQuery
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<EmployeeResponseModel> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees
                .Include(e => e.WorkIntervals)
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            return _mapper.Map<EmployeeResponseModel>(employee);
        }
    }

}
