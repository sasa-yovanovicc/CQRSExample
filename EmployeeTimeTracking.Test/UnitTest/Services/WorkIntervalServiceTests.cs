using AutoMapper;
using EmployeeTimeTracking.Models;
using EmployeeTimeTracking.Models.Constants;
using EmployeeTimeTracking.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace EmployeeTimeTracking.Test.UnitTest.Services
{
    public class WorkIntervalServiceTests
    {
        private readonly WorkIntervalService _service;

        public WorkIntervalServiceTests()
        {
            var mapperMock = new Mock<IMapper>();
            _service = new WorkIntervalService(mapperMock.Object);
        }

        [Fact]
        public void GetWorkIntervalsWithTotalHours_HandlesVariousIntervalCases_Correctly()
        {
            // Arrange: Intervali with overlaping
            var intervals = new List<WorkIntervalResponseModel>
            {
                new WorkIntervalResponseModel { Start = new DateTime(2024, 1, 1, 8, 0, 0), End = new DateTime(2024, 1, 1, 12, 0, 0) }, // Interval 1
                new WorkIntervalResponseModel { Start = new DateTime(2024, 1, 1, 11, 0, 0), End = new DateTime(2024, 1, 1, 15, 0, 0) }, // Interval 2 (overlaps with Interval 1)
                new WorkIntervalResponseModel { Start = new DateTime(2024, 1, 1, 16, 0, 0), End = new DateTime(2024, 1, 1, 18, 0, 0) }  // Interval 3
            };

            var expectedMergedIntervals = new List<WorkIntervalResponseModel>
            {
                new WorkIntervalResponseModel { Start = new DateTime(2024, 1, 1, 8, 0, 0), End = new DateTime(2024, 1, 1, 15, 0, 0) }, // Spojeni Interval 1 i 2
                new WorkIntervalResponseModel { Start = new DateTime(2024, 1, 1, 16, 0, 0), End = new DateTime(2024, 1, 1, 18, 0, 0) }  // Interval 3
            };

            var expectedTotalHours = 9.0; // (4 for the frist interval + 2 hours for the second interval + 2 hours ofr the third interval)

            // Act
            var (sortedIntervals, totalHours, mergedIntervals) = _service.GetWorkIntervalsWithTotalHours(intervals);

            // Assert
            Assert.Equal(expectedMergedIntervals.Count, mergedIntervals.Count);
            for (int i = 0; i < expectedMergedIntervals.Count; i++)
            {
                Assert.Equal(expectedMergedIntervals[i].Start, mergedIntervals[i].Start);
                Assert.Equal(expectedMergedIntervals[i].End, mergedIntervals[i].End);
            }
            Assert.Equal(expectedTotalHours, totalHours);
        }

       
        [Fact]
        public void GetWorkIntervalsWithTotalHours_HandlesNonOverlappingIntervals_Correctly()
        {
            // Arrange: Intervals without overlaping
            var intervals = new List<WorkIntervalResponseModel>
            {
                new WorkIntervalResponseModel { Start = new DateTime(2024, 1, 1, 9, 0, 0), End = new DateTime(2024, 1, 1, 10, 0, 0) }, // Interval 1
                new WorkIntervalResponseModel { Start = new DateTime(2024, 1, 1, 11, 0, 0), End = new DateTime(2024, 1, 1, 12, 0, 0) }  // Interval 2
            };

            var expectedMergedIntervals = new List<WorkIntervalResponseModel>
            {
                new WorkIntervalResponseModel { Start = new DateTime(2024, 1, 1, 9, 0, 0), End = new DateTime(2024, 1, 1, 10, 0, 0) }, // Interval 1
                new WorkIntervalResponseModel { Start = new DateTime(2024, 1, 1, 11, 0, 0), End = new DateTime(2024, 1, 1, 12, 0, 0) }  // Interval 2
            };

            var expectedTotalHours = 2.0; // (1 hour firts interval  + 1 hpur second interval)

            // Act
            var (sortedIntervals, totalHours, mergedIntervals) = _service.GetWorkIntervalsWithTotalHours(intervals);

            // Assert
            Assert.Equal(expectedMergedIntervals.Count, mergedIntervals.Count);
            for (int i = 0; i < expectedMergedIntervals.Count; i++)
            {
                Assert.Equal(expectedMergedIntervals[i].Start, mergedIntervals[i].Start);
                Assert.Equal(expectedMergedIntervals[i].End, mergedIntervals[i].End);
            }
            Assert.Equal(expectedTotalHours, totalHours);
        }
    }
}
