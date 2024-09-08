using AutoMapper;
using EmployeeTimeTracking.Data.Repositories;
using EmployeeTimeTracking.Models;
using EmployeeTimeTracking.Queries;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Handlers
{
    public class GetWorkIntervalsByEmployeeIdHandler : IRequestHandler<GetWorkIntervalsByEmployeeIdQuery, List<WorkIntervalResponseModel>>
    {
        private readonly IWorkIntervalRepository _workIntervalRepository;
        private readonly IMapper _mapper;

        public GetWorkIntervalsByEmployeeIdHandler(IWorkIntervalRepository workIntervalRepository, IMapper mapper)
        {
            _workIntervalRepository = workIntervalRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handler for GetWorkIntervalsByEmployeeIdQuery
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<WorkIntervalResponseModel>> Handle(GetWorkIntervalsByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            // Fetch raw data from the repository
            var intervals = await _workIntervalRepository.GetWorkIntervalsByEmployeeIdAsync(request.EmployeeId);

            // Map to response model
            return _mapper.Map<List<WorkIntervalResponseModel>>(intervals);
        }
    }
}
