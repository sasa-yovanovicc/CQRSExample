using AutoMapper;
using EmployeeTimeTracking.Commands;
using EmployeeTimeTracking.Data;
using EmployeeTimeTracking.Interfaces;
using EmployeeTimeTracking.Models;
using EmployeeTimeTracking.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

public class RecalculateTotalHoursCommandHandler : IRequestHandler<RecalculateTotalHoursCommand, bool>
{
    private readonly ApplicationDbContext _context;
    private readonly IWorkIntervalService _workIntervalService; // Dependency for the service

    public RecalculateTotalHoursCommandHandler(ApplicationDbContext context, IWorkIntervalService workIntervalService)
    {
        _context = context;
        _workIntervalService = workIntervalService;
    }

    /// <summary>
    /// Handler for RecalculateTotalHoursCommand
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(RecalculateTotalHoursCommand request, CancellationToken cancellationToken)
    {
        // Fetch all employees including their work intervals
        var employees = await _context.Employees
            .Include(e => e.WorkIntervals)
            .ToListAsync(cancellationToken);

        foreach (var employee in employees)
        {
            // Map WorkIntervals to WorkIntervalResponseModel (assuming there's a mapping setup)
            var workIntervalModels = employee.WorkIntervals
                .Select(wi => new WorkIntervalResponseModel
                {
                    Start = (DateTime)wi.Start,
                    End = (DateTime)wi.End
                }).ToList();

            // Use the service to get total hours
            var (_, totalHours, _) = _workIntervalService.GetWorkIntervalsWithTotalHours(workIntervalModels);

            // Update the employee's TotalHours
            employee.TotalHours = (decimal)totalHours;

            _context.Employees.Update(employee);
        }

        try
        {
            // Save changes to the database
            var result = await _context.SaveChangesAsync(cancellationToken) > 0;
            return result;
        }
        catch (DbUpdateException ex)
        {
            // Log and handle the exception as needed
            Console.WriteLine(ex.Message);
            throw; // Rethrow or handle as needed
        }
    }
}
