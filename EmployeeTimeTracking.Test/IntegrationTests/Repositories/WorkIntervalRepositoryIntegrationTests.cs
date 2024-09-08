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
    public class WorkIntervalRepositoryIntegrationTests
    {
        private readonly ApplicationDbContext _context;
        private readonly WorkIntervalRepository _repository;

        public WorkIntervalRepositoryIntegrationTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "WorkIntervalTestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _context.Database.EnsureCreated();
            _repository = new WorkIntervalRepository(_context);
        }

        [Fact]
        public async Task GetWorkIntervalsByEmployeeIdAsync_ReturnsIntervals_WhenIntervalsExist()
        {
            // Arrange
            var employee = new Employee { FirstName = "John", LastName = "Doe", Position = "Developer" };
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            var workIntervals = new List<WorkInterval>
            {
                new WorkInterval { Employee = employee, Start = new DateTime(2024, 1, 1, 9, 0, 0), End = new DateTime(2024, 1, 1, 13, 0, 0) },
                new WorkInterval { Employee = employee, Start = new DateTime(2024, 1, 1, 14, 0, 0), End = new DateTime(2024, 1, 1, 17, 0, 0) }
            };
            _context.WorkIntervals.AddRange(workIntervals);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetWorkIntervalsByEmployeeIdAsync(employee.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, wi => wi.Start == new DateTime(2024, 1, 1, 9, 0, 0));
            Assert.Contains(result, wi => wi.End == new DateTime(2024, 1, 1, 17, 0, 0));
        }

        [Fact]
        public async Task GetWorkIntervalsByEmployeeIdAsync_ReturnsEmptyList_WhenNoIntervalsExist()
        {
            // Arrange
            var employeeId = 2; // This ID does not exist in the database

            // Act
            var result = await _repository.GetWorkIntervalsByEmployeeIdAsync(employeeId);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetWorkIntervalsByEmployeeIdAsync_ReturnsIntervalsSortedByStart()
        {
            // Arrange
            var employee = new Employee { FirstName = "John", LastName = "Doe", Position = "Developer" };
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            var workIntervals = new List<WorkInterval>
            {
                new WorkInterval { Employee = employee, Start = new DateTime(2024, 1, 1, 15, 0, 0), End = new DateTime(2024, 1, 1, 16, 0, 0) },
                new WorkInterval { Employee = employee, Start = new DateTime(2024, 1, 1, 9, 0, 0), End = new DateTime(2024, 1, 1, 13, 0, 0) }
            };
            _context.WorkIntervals.AddRange(workIntervals);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetWorkIntervalsByEmployeeIdAsync(employee.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(new DateTime(2024, 1, 1, 9, 0, 0), result.First().Start);
            Assert.Equal(new DateTime(2024, 1, 1, 15, 0, 0), result.Last().Start);
        }
    }
}
