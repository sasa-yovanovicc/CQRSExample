using EmployeeTimeTracking.Models;
using FluentValidation;

namespace EmployeeTimeTracking.Validators
{
    public class WorkIntervalValidator : AbstractValidator<WorkIntervalRequestModel>
    {
        public WorkIntervalValidator()
        {
            RuleFor(x => x.EmployeeId).NotEmpty().WithMessage("Employee ID is required.");
            RuleFor(x => x.Start).LessThan(x => x.End).WithMessage("Start time must be before end time.");
            RuleFor(x => x.End).GreaterThan(x => x.Start).WithMessage("End time must be after start time.");
        }
    }

}
