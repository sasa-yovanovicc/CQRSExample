using AutoMapper;
using EmployeeTimeTracking.Commands;
using EmployeeTimeTracking.Controllers;
using EmployeeTimeTracking.Interfaces;
using EmployeeTimeTracking.Models;
using EmployeeTimeTracking.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeTimeTracking.Test.UnitTest.Controllers
{
    public class WorkIntervalControllerTests
    {
        private readonly WorkIntervalController _controller;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IWorkIntervalService> _workIntervalServiceMock;

        public WorkIntervalControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _mapperMock = new Mock<IMapper>();
            _workIntervalServiceMock = new Mock<IWorkIntervalService>();

            _controller = new WorkIntervalController(
                _mediatorMock.Object,
                _mapperMock.Object,
                _workIntervalServiceMock.Object);
        }

        [Fact]
        public async Task Start_Get_ReturnsNotFound_WhenEmployeeIsNull()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetEmployeeByIdQuery>(), default))
                         .ReturnsAsync((EmployeeResponseModel)null);

            // Act
            var result = await _controller.Start(null, 1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Start_Get_ReturnsViewResult_WithValidModel()
        {
            // Arrange
            var employee = new EmployeeResponseModel { Id = 1, FirstName = "John", LastName = "Doe", Position = "Developer" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetEmployeeByIdQuery>(), default))
                         .ReturnsAsync(employee);
            _mapperMock.Setup(m => m.Map<WorkIntervalRequestModel>(employee))
                       .Returns(new WorkIntervalRequestModel { EmployeeId = 1, FirstName = "John", LastName = "Doe", Position = "Developer" });

            // Act
            var result = await _controller.Start(null, 1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<WorkIntervalRequestModel>(viewResult.Model);
            Assert.Equal(1, model.EmployeeId);
            Assert.Equal("John", model.FirstName);
            Assert.Equal("Doe", model.LastName);
        }

        [Fact]
        public async Task Start_Get_ReturnsViewResult_WithExistingModel_WhenIdIsProvided()
        {
            // Arrange
            var employee = new EmployeeResponseModel { Id = 1, FirstName = "John", LastName = "Doe", Position = "Developer" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetEmployeeByIdQuery>(), default))
                         .ReturnsAsync(employee);
            _mapperMock.Setup(m => m.Map<WorkIntervalRequestModel>(employee))
                       .Returns(new WorkIntervalRequestModel { Id = 1, EmployeeId = 1, FirstName = "John", LastName = "Doe", Position = "Developer" });

            // Act
            var result = await _controller.Start(1, 1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<WorkIntervalRequestModel>(viewResult.Model);
            Assert.Equal(1, model.Id);
            Assert.Equal(1, model.EmployeeId);
            Assert.Equal("John", model.FirstName);
            Assert.Equal("Doe", model.LastName);
        }

        [Fact]
        public async Task Start_Post_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            // Arrange
            var model = new WorkIntervalSaveRequestModel
            {
                EmployeeId = 1,
                Start = DateTime.UtcNow
            };
            _controller.ModelState.AddModelError("Start", "Required");

            // Act
            var result = await _controller.SaveStart(model);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<WorkIntervalSaveRequestModel>(viewResult.Model);
        }

        [Fact]
        public async Task Start_Post_RedirectsToIndex_WhenCommandSucceeds()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<AddWorkIntervalCommand>(), default))
                         .ReturnsAsync(true);

            var model = new WorkIntervalSaveRequestModel
            {
                EmployeeId = 1,
                Start = DateTime.Now
            };

            // Act
            var result = await _controller.SaveStart(model);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("Employee", redirectToActionResult.ControllerName);
        }

        [Fact]
        public async Task Start_Post_ReturnsViewResult_WithModelError_WhenCommandFails()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<AddWorkIntervalCommand>(), default))
                         .ReturnsAsync(false);

            var model = new WorkIntervalSaveRequestModel
            {
                EmployeeId = 1,
                Start = DateTime.Now
            };

            // Act
            var result = await _controller.SaveStart(model);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<WorkIntervalSaveRequestModel>(viewResult.Model);
            Assert.True(_controller.ModelState.ContainsKey(""));
            Assert.Equal("Unable to start work interval.", _controller.ModelState[""].Errors[0].ErrorMessage);
        }

        [Fact]
        public async Task ListWorkIntervals_ReturnsViewResult_WithMergedIntervals()
        {
            // Arrange
            var employeeId = 1;
            var intervals = new List<WorkIntervalResponseModel>
            {
                new WorkIntervalResponseModel { Start = new DateTime(2024, 1, 1, 9, 0, 0), End = new DateTime(2024, 1, 1, 13, 0, 0) },
                new WorkIntervalResponseModel { Start = new DateTime(2024, 1, 1, 11, 0, 0), End = new DateTime(2024, 1, 1, 17, 0, 0) }
            };
            var sortedIntervals = new List<WorkIntervalResponseModel>
            {
                new WorkIntervalResponseModel { Start = new DateTime(2024, 1, 1, 9, 0, 0), End = new DateTime(2024, 1, 1, 13, 0, 0) },
                new WorkIntervalResponseModel { Start = new DateTime(2024, 1, 1, 11, 0, 0), End = new DateTime(2024, 1, 1, 17, 0, 0) }
            };
            var mergedIntervals = new List<WorkIntervalResponseModel>
            {
                new WorkIntervalResponseModel { Start = new DateTime(2024, 1, 1, 9, 0, 0), End = new DateTime(2024, 1, 1, 17, 0, 0) }
            };

            var expectedTotalHours = 8.0;

            // Setup the mocks
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetWorkIntervalsByEmployeeIdQuery>(), default))
                         .ReturnsAsync(intervals);

            _workIntervalServiceMock.Setup(s => s.GetWorkIntervalsWithTotalHours(intervals))
                                     .Returns((sortedIntervals, expectedTotalHours, mergedIntervals));

            // Act
            var result = await _controller.ListWorkIntervals(employeeId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);

            // Check if the ViewResult's model is a tuple
            var model = Assert.IsType<ValueTuple<List<WorkIntervalResponseModel>, double, int>>(viewResult.Model);

            // Validate the list of intervals
            var intervalsList = model.Item1;
            Assert.Equal(new DateTime(2024, 1, 1, 9, 0, 0), intervalsList.First().Start);
            Assert.Equal(new DateTime(2024, 1, 1, 13, 0, 0), intervalsList.First().End);
            Assert.Equal(new DateTime(2024, 1, 1, 11, 0, 0), intervalsList.Last().Start);
            Assert.Equal(new DateTime(2024, 1, 1, 17, 0, 0), intervalsList.Last().End);

            // Validate the total hours
            Assert.Equal(expectedTotalHours, model.Item2);

            // Validate the employee ID
            Assert.Equal(employeeId, model.Item3);
        }

        [Fact]
        public async Task End_Get_ReturnsNotFound_WhenEmployeeIsNull()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetEmployeeByIdQuery>(), default))
                         .ReturnsAsync((EmployeeResponseModel)null);

            // Act
            var result = await _controller.End(null, 1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task End_Get_ReturnsViewResult_WithValidModel()
        {
            // Arrange
            var employee = new EmployeeResponseModel { Id = 1, FirstName = "John", LastName = "Doe", Position = "Developer" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetEmployeeByIdQuery>(), default))
                         .ReturnsAsync(employee);
            _mapperMock.Setup(m => m.Map<WorkIntervalRequestModel>(employee))
                       .Returns(new WorkIntervalRequestModel { EmployeeId = 1, FirstName = "John", LastName = "Doe", Position = "Developer" });

            // Act
            var result = await _controller.End(null, 1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<WorkIntervalRequestModel>(viewResult.Model);
            Assert.Equal(1, model.EmployeeId);
            Assert.Equal("John", model.FirstName);
            Assert.Equal("Doe", model.LastName);
        }

        [Fact]
        public async Task End_Get_ReturnsViewResult_WithExistingModel_WhenIdIsProvided()
        {
            // Arrange
            var employee = new EmployeeResponseModel { Id = 1, FirstName = "John", LastName = "Doe", Position = "Developer" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetEmployeeByIdQuery>(), default))
                         .ReturnsAsync(employee);
            _mapperMock.Setup(m => m.Map<WorkIntervalRequestModel>(employee))
                       .Returns(new WorkIntervalRequestModel { Id = 1, EmployeeId = 1, FirstName = "John", LastName = "Doe", Position = "Developer" });

            // Act
            var result = await _controller.End(1, 1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<WorkIntervalRequestModel>(viewResult.Model);
            Assert.Equal(1, model.Id);
            Assert.Equal(1, model.EmployeeId);
            Assert.Equal("John", model.FirstName);
            Assert.Equal("Doe", model.LastName);
        }
    }
}
