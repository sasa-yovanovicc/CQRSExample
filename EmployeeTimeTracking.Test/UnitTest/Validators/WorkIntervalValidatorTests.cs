using EmployeeTimeTracking.Models;
using EmployeeTimeTracking.Validators;
using FluentValidation.TestHelper;
using Xunit;
using System;

namespace EmployeeTimeTracking.Test.UnitTest.Validators
{
    public class WorkIntervalValidatorTests
    {
        private readonly WorkIntervalValidator _validator;

        public WorkIntervalValidatorTests()
        {
            _validator = new WorkIntervalValidator();
        }

        [Fact]
        public void Should_Have_Error_When_EmployeeId_Is_Empty()
        {
            // Arrange
            var model = new WorkIntervalRequestModel
            {
                EmployeeId = 0, // Assuming 0 is considered invalid here
                FirstName = "John",
                LastName = "Doe",
                Position = "Developer",
                Start = DateTime.Now.AddHours(-1),
                End = DateTime.Now
            };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.EmployeeId)
                  .WithErrorMessage("Employee ID is required.");
        }

        [Fact]
        public void Should_Have_Error_When_Start_Is_Not_Before_End()
        {
            // Arrange
            var model = new WorkIntervalRequestModel
            {
                EmployeeId = 1,
                FirstName = "John",
                LastName = "Doe",
                Position = "Developer",
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(-1)
            };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Start)
                  .WithErrorMessage("Start time must be before end time.");
            result.ShouldHaveValidationErrorFor(x => x.End)
                  .WithErrorMessage("End time must be after start time.");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Model_Is_Valid()
        {
            // Arrange
            var model = new WorkIntervalRequestModel
            {
                EmployeeId = 1,
                FirstName = "John",
                LastName = "Doe",
                Position = "Developer",
                Start = DateTime.Now.AddHours(-1),
                End = DateTime.Now
            };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
