using AutoMapper;
using EmployeeTimeTracking.Commands;
using EmployeeTimeTracking.Data.Entities;
using EmployeeTimeTracking.Data;
using EmployeeTimeTracking.Interfaces;
using EmployeeTimeTracking.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class AddWorkIntervalCommandHandler : IRequestHandler<AddWorkIntervalCommand, bool>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IWorkIntervalService _workIntervalService;

    public AddWorkIntervalCommandHandler(ApplicationDbContext context, IMapper mapper, IWorkIntervalService workIntervalService)
    {
        _context = context;
        _mapper = mapper;
        _workIntervalService = workIntervalService;
    }

    /// <summary>
    /// Handler for AddWorkIntervalCommand
    /// </summary>
    public async Task<bool> Handle(AddWorkIntervalCommand request, CancellationToken cancellationToken)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var workInterval = _mapper.Map<WorkInterval>(request.WorkInterval);
            _context.WorkIntervals.Add(workInterval);
            await _context.SaveChangesAsync();

            // Fetch employee and calculate new total hours
            var employee = await _context.Employees
                .Include(e => e.WorkIntervals)
                .FirstOrDefaultAsync(e => e.Id == workInterval.EmployeeId);

            if (employee != null)
            {
                var intervals = employee.WorkIntervals.Select(wi => _mapper.Map<WorkIntervalResponseModel>(wi)).ToList();
                var (_, totalHours, _) = _workIntervalService.GetWorkIntervalsWithTotalHours(intervals);
                employee.TotalHours = (decimal?)totalHours;

                _context.Employees.Update(employee);
                await _context.SaveChangesAsync();
            }

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
