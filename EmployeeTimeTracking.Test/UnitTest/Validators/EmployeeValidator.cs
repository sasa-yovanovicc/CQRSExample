using EmployeeTimeTracking.Models;
using EmployeeTimeTracking.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace EmployeeTimeTracking.Test.UnitTest.Validators
{
    public class EmployeeValidatorTests
    {
        private readonly EmployeeValidator _validator;

        public EmployeeValidatorTests()
        {
            _validator = new EmployeeValidator();
        }

        [Fact]
        public void Should_Have_Error_When_FirstName_Is_Empty()
        {
            // Arrange
            var model = new EmployeeRequestModel { FirstName = "", LastName = "Doe", Position = "Developer", HireDate = DateTime.Now.AddYears(-1) };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.FirstName)
                .WithErrorMessage("First Name is required.");
        }

        [Fact]
        public void Should_Have_Error_When_LastName_Is_Empty()
        {
            // Arrange
            var model = new EmployeeRequestModel { FirstName = "John", LastName = "", Position = "Developer", HireDate = DateTime.Now.AddYears(-1) };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.LastName)
                .WithErrorMessage("Last Name is required.");
        }

        [Fact]
        public void Should_Have_Error_When_Position_Is_Empty()
        {
            // Arrange
            var model = new EmployeeRequestModel { FirstName = "John", LastName = "Doe", Position = "", HireDate = DateTime.Now.AddYears(-1) };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Position)
                .WithErrorMessage("Position is required.");
        }

        [Fact]
        public void Should_Have_Error_When_HireDate_Is_In_Future()
        {
            // Arrange
            var model = new EmployeeRequestModel { FirstName = "John", LastName = "Doe", Position = "Developer", HireDate = DateTime.Now.AddDays(1) };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.HireDate)
                .WithErrorMessage("Hire Date cannot be in the future.");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Model_Is_Valid()
        {
            // Arrange
            var model = new EmployeeRequestModel { FirstName = "John", LastName = "Doe", Position = "Developer", HireDate = DateTime.Now.AddYears(-1) };

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.FirstName);
            result.ShouldNotHaveValidationErrorFor(x => x.LastName);
            result.ShouldNotHaveValidationErrorFor(x => x.Position);
            result.ShouldNotHaveValidationErrorFor(x => x.HireDate);
        }
    }
}
