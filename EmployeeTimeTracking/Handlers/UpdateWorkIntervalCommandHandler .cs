using AutoMapper;
using EmployeeTimeTracking.Commands;
using EmployeeTimeTracking.Data;
using EmployeeTimeTracking.Interfaces;
using EmployeeTimeTracking.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class UpdateWorkIntervalCommandHandler : IRequestHandler<UpdateWorkIntervalCommand, bool>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IWorkIntervalService _workIntervalService;

    public UpdateWorkIntervalCommandHandler(ApplicationDbContext context, IMapper mapper, IWorkIntervalService workIntervalService)
    {
        _context = context;
        _mapper = mapper;
        _workIntervalService = workIntervalService;
    }

    /// <summary>
    /// Handler for UpdateWorkIntervalCommand
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(UpdateWorkIntervalCommand request, CancellationToken cancellationToken)
    {
        var workInterval = await _context.WorkIntervals.FindAsync(request.WorkInterval.Id);
        if (workInterval == null)
        {
            return false;
        }

        workInterval.Start = request.WorkInterval.Start;
        workInterval.End = request.WorkInterval.End;

        _context.WorkIntervals.Update(workInterval);
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

        return true;
    }
}
