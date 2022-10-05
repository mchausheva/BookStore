using BookStore.Models.Requests;
using FluentValidation;

namespace BookStore.Validations
{
    public class PersonRequestValidator : AbstractValidator<AddPersonRequest>
    {
        public PersonRequestValidator()
        {
            RuleFor(x => x.Age).GreaterThan(0).WithMessage("My custom message for Age zero")
                               .LessThanOrEqualTo(120).WithMessage("My custom message for Age 120");
            RuleFor(x => x.FirstName).NotEmpty()
                                .MinimumLength(3)
                                .MaximumLength(30);
            RuleFor(x => x.LastName).NotEmpty()
                            .MaximumLength(40)
                            .MinimumLength(5);
            RuleFor(x => x.DateOfBirth)
                    .GreaterThan(DateTime.MinValue)
                    .LessThan(DateTime.MaxValue);
        }
    }
}
