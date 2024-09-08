using System;
using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Xunit;

namespace EmployeeTimeTracking.Test.UnitTest.Validators
{
    public class DateGreaterThanAttributeTests
    {
        private class TestModel
        {
            [DateGreaterThan("StartDate", ErrorMessage = "EndDate must be after StartDate.")]
            public DateTime EndDate { get; set; }

            public DateTime StartDate { get; set; }
        }

        [Fact]
        public void Should_Return_Error_When_EndDate_Is_Not_After_StartDate()
        {
            // Arrange
            var model = new TestModel
            {
                StartDate = new DateTime(2024, 1, 1),
                EndDate = new DateTime(2024, 1, 1) // Same as StartDate, should trigger error
            };

            var context = new ValidationContext(model) { MemberName = "EndDate" };
            var attribute = new DateGreaterThanAttribute("StartDate")
            {
                ErrorMessage = "EndDate must be after StartDate."
            };

            // Act
            var result = attribute.GetValidationResult(model.EndDate, context);

            // Assert
            result.Should().BeEquivalentTo(new ValidationResult("EndDate must be after StartDate."));
        }

        [Fact]
        public void Should_Return_Success_When_EndDate_Is_After_StartDate()
        {
            // Arrange
            var model = new TestModel
            {
                StartDate = new DateTime(2024, 1, 1),
                EndDate = new DateTime(2024, 1, 2) // After StartDate, should not trigger error
            };

            var context = new ValidationContext(model) { MemberName = "EndDate" };
            var attribute = new DateGreaterThanAttribute("StartDate")
            {
                ErrorMessage = "EndDate must be after StartDate."
            };

            // Act
            var result = attribute.GetValidationResult(model.EndDate, context);

            // Assert
            result.Should().Be(ValidationResult.Success);
        }

        [Fact]
        public void Should_Return_Error_When_ComparisonProperty_Does_Not_Exist()
        {
            // Arrange
            var model = new TestModel
            {
                StartDate = new DateTime(2024, 1, 1),
                EndDate = new DateTime(2024, 1, 2)
            };

            var context = new ValidationContext(model) { MemberName = "EndDate" };
            var attribute = new DateGreaterThanAttribute("NonExistentProperty");

            // Act
            var result = attribute.GetValidationResult(model.EndDate, context);

            // Assert
            result.Should().BeEquivalentTo(new ValidationResult("Unknown property: NonExistentProperty"));
        }
    }
}
