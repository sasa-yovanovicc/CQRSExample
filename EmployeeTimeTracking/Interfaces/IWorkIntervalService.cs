using EmployeeTimeTracking.Models;
using System;
using System.Collections.Generic;

namespace EmployeeTimeTracking.Interfaces
{
    public interface IWorkIntervalService
    {
        /// <summary>
        /// Gets the list of work intervals and calculates the total hours worked, considering overlaps.
        /// </summary>
        /// <param name="intervals">The list of work intervals to process.</param>
        /// <returns>A tuple containing the list of work intervals and the total hours worked.</returns>
        (List<WorkIntervalResponseModel> SortedIntervals, double TotalHours, List<WorkIntervalResponseModel> MergedIntervals) GetWorkIntervalsWithTotalHours(List<WorkIntervalResponseModel> intervals);
    }
}
