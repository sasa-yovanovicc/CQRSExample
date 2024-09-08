using EmployeeTimeTracking.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTimeTracking.Data.Repositories
{
    public class WorkIntervalRepository : IWorkIntervalRepository
    {
        private readonly ApplicationDbContext _context;

        public WorkIntervalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<WorkInterval>> GetWorkIntervalsByEmployeeIdAsync(int employeeId)
        {
            return await _context.WorkIntervals
                .Where(wi => wi.EmployeeId == employeeId)
                .OrderBy(wi => wi.Start)
                .ToListAsync();
        }
    }
}
