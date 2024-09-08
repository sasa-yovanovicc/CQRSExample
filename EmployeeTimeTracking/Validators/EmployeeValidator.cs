using EmployeeTimeTracking.Models;
using FluentValidation;

namespace EmployeeTimeTracking.Validators
{
    public class EmployeeValidator : AbstractValidator<EmployeeRequestModel>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name is required.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name is required.");
            RuleFor(x => x.Position).NotEmpty().WithMessage("Position is required.");
            RuleFor(x => x.HireDate).LessThan(DateTime.Now).WithMessage("Hire Date cannot be in the future.");
        }
    }

}
