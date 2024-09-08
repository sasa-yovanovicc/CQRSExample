using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeTimeTracking.Data.Entities;
using EmployeeTimeTracking.Models;

namespace EmployeeTimeTracking.Data.Repositories
{
    public interface IWorkIntervalRepository
    {
        Task<List<WorkInterval>> GetWorkIntervalsByEmployeeIdAsync(int employeeId);
    }
}

