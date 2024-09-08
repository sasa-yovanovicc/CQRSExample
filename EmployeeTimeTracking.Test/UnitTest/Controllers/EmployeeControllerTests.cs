using EmployeeTimeTracking.Controllers;
using EmployeeTimeTracking.Models;
using EmployeeTimeTracking.Queries;
using EmployeeTimeTracking.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeTimeTracking.Test.UnitTest.Controllers
{
    public class EmployeeControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly EmployeeController _controller;

        public EmployeeControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new EmployeeController(_mediatorMock.Object);
        }

        [Fact]
        public async Task Index_ReturnsViewResult_WithListOfEmployees()
        {
            // Arrange
            var employees = new List<EmployeeResponseModel>
            {
                new EmployeeResponseModel { Id = 1, FirstName = "John", LastName = "Doe" }
            };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllEmployeesQuery>(), default)).ReturnsAsync(employees);

            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<EmployeeResponseModel>>(viewResult.ViewData.Model);
            Assert.Single(model);
        }

        [Fact]
        public async Task Details_ReturnsViewResult_WhenEmployeeExists()
        {
            // Arrange
            var employee = new EmployeeResponseModel { Id = 1, FirstName = "John", LastName = "Doe" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetEmployeeQuery>(), default)).ReturnsAsync(employee);

            // Act
            var result = await _controller.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<EmployeeResponseModel>(viewResult.ViewData.Model);
            Assert.Equal(1, model.Id);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenEmployeeDoesNotExist()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetEmployeeQuery>(), default)).ReturnsAsync((EmployeeResponseModel)null);

            // Act
            var result = await _controller.Details(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_Post_RedirectsToIndex_WhenModelStateIsValid()
        {
            // Arrange
            var model = new EmployeeRequestModel { Id = 1, FirstName = "John", LastName = "Doe", Position = "Developer" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<AddEmployeeCommand>(), default)).ReturnsAsync(true);

            // Act
            var result = await _controller.Create(model);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Create_Post_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            // Arrange
            var model = new EmployeeRequestModel
            {
                FirstName = "John",
                LastName = "Doe",
                Position = "Developer"
            };

            _controller.ModelState.AddModelError("FirstName", "Required");

            // Act
            var result = await _controller.Create(model);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<EmployeeRequestModel>(viewResult.ViewData.Model);
        }


        [Fact]
        public async Task Edit_Post_RedirectsToIndex_WhenModelStateIsValid()
        {
            // Arrange
            var model = new EmployeeRequestModel { Id = 1, FirstName = "John", LastName = "Doe", Position = "Developer" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateEmployeeCommand>(), default)).ReturnsAsync(true);

            // Act
            var result = await _controller.Edit(1, model);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Edit_Post_ReturnsNotFound_WhenIdDoesNotMatchModelId()
        {
            // Arrange
            var model = new EmployeeRequestModel
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Doe",
                Position = "Designer"
            };

            // Act
            var result = await _controller.Edit(1, model);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task RecalculateTotalHours_RedirectsToIndex_WhenCommandSucceeds()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<RecalculateTotalHoursCommand>(), default)).ReturnsAsync(true);

            // Act
            var result = await _controller.RecalculateTotalHours();

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task RecalculateTotalHours_RedirectsToIndex_WhenCommandFails()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<RecalculateTotalHoursCommand>(), default)).ReturnsAsync(false);

            // Act
            var result = await _controller.RecalculateTotalHours();

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.True(_controller.ModelState.ContainsKey(""));
        }

    }
}
