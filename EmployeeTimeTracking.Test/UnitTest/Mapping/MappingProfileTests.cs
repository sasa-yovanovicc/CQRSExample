using AutoMapper;
using EmployeeTimeTracking.Data.Entities;
using EmployeeTimeTracking.Mapping;
using EmployeeTimeTracking.Models;
using Xunit;

namespace EmployeeTimeTracking.Test.UnitTest.Mapping
{
    public class MappingProfileTests
    {
        private readonly IMapper _mapper;

        public MappingProfileTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public void MappingConfiguration_IsValid()
        {
            // Act & Assert
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void Should_Map_EmployeeRequestModel_To_Employee()
        {
            // Arrange
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = config.CreateMapper();
            var employeeRequestModel = new EmployeeRequestModel
            {
                FirstName = "John",
                LastName = "Doe",
                Position = "Developer"
            };

            // Act
            var employee = mapper.Map<Employee>(employeeRequestModel);

            // Assert
            Assert.NotNull(employee);
        }

        [Fact]
        public void Should_Map_Employee_To_EmployeeResponseModel()
        {
            // Arrange
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = config.CreateMapper();
            var employee = new Employee
            {
                FirstName = "John",
                LastName = "Doe",
                Position = "Developer"
            };

            // Act
            var responseModel = mapper.Map<EmployeeResponseModel>(employee);

            // Assert
            Assert.NotNull(responseModel);
        }

        [Fact]
        public void Should_Map_WorkIntervalRequestModel_To_WorkInterval()
        {
            // Arrange
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = config.CreateMapper();
            var workIntervalRequestModel = new WorkIntervalRequestModel
            {
                FirstName = "John",
                LastName = "Doe",
                Position = "Developer"
            };

            // Act
            var workInterval = mapper.Map<WorkInterval>(workIntervalRequestModel);

            // Assert
            Assert.NotNull(workInterval);
        }

        [Fact]
        public void Should_Map_WorkInterval_To_WorkIntervalResponseModel()
        {
            // Arrange
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var employee = new Employee
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Position = "Deveoper"
            };

            var workInterval = new WorkInterval
            {
                Id = 1,
                EmployeeId = 1,
                Start = DateTime.Now.AddHours(-1),
                End = DateTime.Now,
                Employee = employee  
            };

            // Act
            var responseModel = _mapper.Map<WorkIntervalResponseModel>(workInterval);

            // Assert
            Assert.NotNull(responseModel);
            Assert.Equal(workInterval.Start, responseModel.Start);
            Assert.Equal(workInterval.End, responseModel.End);
            Assert.Equal(workInterval.EmployeeId, responseModel.EmployeeId);
        }
    }
}
