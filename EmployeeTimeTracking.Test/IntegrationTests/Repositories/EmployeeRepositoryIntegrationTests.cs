using EmployeeTimeTracking.Data;
using EmployeeTimeTracking.Data.Entities;
using EmployeeTimeTracking.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeTimeTracking.Test.IntegrationTests.Repositories
{
    public class EmployeeRepositoryIntegrationTests
    {
        private readonly ApplicationDbContext _context;
        private readonly EmployeeRepository _repository;

        public EmployeeRepositoryIntegrationTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "EmployeeTestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _context.Database.EnsureCreated();
            _repository = new EmployeeRepository(_context);
        }

        [Fact]
        public async Task GetEmployeeByIdAsync_ReturnsEmployee_WhenEmployeeExists()
        {
            // Arrange
            var employee = new Employee { FirstName = "John", LastName = "Doe", Position = "Developer" };
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetEmployeeByIdAsync(employee.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John", result.FirstName);
        }

        [Fact]
        public async Task GetAllEmployeesAsync_ReturnsAllEmployees()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee { FirstName = "John", LastName = "Doe", Position = "Developer" },
                new Employee { FirstName = "Jane", LastName = "Doe", Position = "Designer" }
            };
            _context.Employees.AddRange(employees);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllEmployeesAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, e => e.FirstName == "John" && e.LastName == "Doe");
            Assert.Contains(result, e => e.FirstName == "Jane" && e.LastName == "Doe");
        }
    }
}
