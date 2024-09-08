using AutoMapper;
using EmployeeTimeTracking.Commands;
using EmployeeTimeTracking.Data;
using EmployeeTimeTracking.Data.Entities;
using MediatR;

namespace EmployeeTimeTracking.Handlers
{
    /// <summary>
    /// Handler for AddEmployeeCommand
    /// </summary>
    public class AddEmployeeCommandHandler : IRequestHandler<AddEmployeeCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AddEmployeeCommandHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var employee = _mapper.Map<Employee>(request.Employee);
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }

}
