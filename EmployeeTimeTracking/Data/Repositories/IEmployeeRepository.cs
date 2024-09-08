using EmployeeTimeTracking.Data.Entities;

namespace EmployeeTimeTracking.Data.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task SaveChangesAsync();
    }
}
