using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using EmployeeTimeTracking.Data;
using EmployeeTimeTracking.Data.Entities;
using EmployeeTimeTracking.Data.Repositories;

namespace EmployeeTimeTracking.Tests.UnitTest.Repositories
{
    public class EmployeeRepositoryTests
    {
        private readonly EmployeeRepository _repository;
        private readonly ApplicationDbContext _context;

        public EmployeeRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);

            // Ensure database is created and seed initial data
            _context.Database.EnsureCreated();
            SeedDatabase();

            _repository = new EmployeeRepository(_context);
        }

        private void SeedDatabase()
        {
            if (_context.Employees.Any())
            {
                // Database has already been seeded
                return;
            }

            var employees = new List<Employee>
            {
                new Employee { Id = 1, FirstName = "John", LastName = "Doe", Position = "Developer", HireDate = DateTime.Now },
                new Employee { Id = 2, FirstName = "Jane", LastName = "Doe", Position = "Designer", HireDate = DateTime.Now }
            };

            _context.Employees.AddRange(employees);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetEmployeeByIdAsync_ReturnsEmployee_WhenEmployeeExists()
        {
            // Act
            var result = await _repository.GetEmployeeByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John", result.FirstName);
        }

        [Fact]
        public async Task GetAllEmployeesAsync_ReturnsAllEmployees()
        {
            // Act
            var result = await _repository.GetAllEmployeesAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, e => e.FirstName == "John" && e.LastName == "Doe");
            Assert.Contains(result, e => e.FirstName == "Jane" && e.LastName == "Doe");
        }
    }
}
