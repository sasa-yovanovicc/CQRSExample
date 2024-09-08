using EmployeeTimeTracking.Data.Repositories;
using EmployeeTimeTracking.Queries;
using EmployeeTimeTracking.Models;
using MediatR;
using AutoMapper;

namespace EmployeeTimeTracking.Handlers
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeResponseModel>
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;

        public GetEmployeeByIdQueryHandler(IEmployeeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handler for GetEmployeeByIdQuery
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<EmployeeResponseModel> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _repository.GetEmployeeByIdAsync(request.Id);
            return _mapper.Map<EmployeeResponseModel>(employee);
        }
    }
}
