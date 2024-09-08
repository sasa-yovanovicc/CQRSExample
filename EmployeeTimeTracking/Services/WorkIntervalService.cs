using AutoMapper;
using EmployeeTimeTracking.Interfaces;
using EmployeeTimeTracking.Models;
using EmployeeTimeTracking.Models.Constants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeTimeTracking.Services
{
    public class WorkIntervalService : IWorkIntervalService
    {
        private readonly IMapper _mapper;

        public WorkIntervalService(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// GetWorkIntervalsWithTotalHours
        /// </summary>
        /// <param name="intervals"></param>
        /// <returns>Sorted Work Intervals, total Hours decimal value and Merged Work Intervals (not in use in thsi vwrsion)</returns>
        public (List<WorkIntervalResponseModel> SortedIntervals, double TotalHours, List<WorkIntervalResponseModel> MergedIntervals) GetWorkIntervalsWithTotalHours(List<WorkIntervalResponseModel> intervals)
        {
            if (intervals == null || !intervals.Any())
            {
                return (new List<WorkIntervalResponseModel>(), 0.0, new List<WorkIntervalResponseModel>());
            }

            // Sort intervals by start time
            var sortedIntervals = intervals
                .OrderBy(interval => interval.Start)
                .ToList();

            // Filter out intervals with invalid dates
            var validIntervals = sortedIntervals
                .Where(interval => interval.Start != DateConstants.MinDate && interval.Start != DateConstants.SpecialDate &&
                                   interval.End != DateConstants.MinDate && interval.End != DateConstants.SpecialDate)
                .ToList();

            if (!validIntervals.Any())
            {
                return (sortedIntervals, 0.0, sortedIntervals); // Return sorted intervals as-is
            }

            var mergedIntervals = new List<WorkIntervalResponseModel>();
            var totalHours = 0.0;

            var current = validIntervals.First();

            foreach (var interval in validIntervals.Skip(1))
            {
                if (interval.Start <= current.End)
                {
                    // Overlapping intervals, merge them
                    current.End = new DateTime(Math.Max(current.End.Ticks, interval.End.Ticks));
                }
                else
                {
                    // Non-overlapping interval, save the current one and move to the next
                    mergedIntervals.Add(current);
                    // Calculate the duration for the non-overlapping interval
                    totalHours += (current.End - current.Start).TotalHours;
                    current = interval;
                }
            }

            // Add the last interval and its duration
            mergedIntervals.Add(current);
            totalHours += (current.End - current.Start).TotalHours;

            return (sortedIntervals, totalHours, mergedIntervals); // Return merged intervals and total hours
        }
    }
}
