using BookStore.Models.Requests;
using FluentValidation;

namespace BookStore.Validations
{
    public class AuthorRequestValidator : AbstractValidator<AddAuthorRequest>
    {
        public AuthorRequestValidator()
        {
            RuleFor(x => x.Age).GreaterThan(0).WithMessage("My custom message for Age zero")
                               .LessThanOrEqualTo(120).WithMessage("My custom message for Age 120");
            RuleFor(x => x.Name).NotEmpty()
                                .MinimumLength(3)
                                .MaximumLength(30);
            When(x => !string.IsNullOrEmpty(x.Nickname),() =>
            {
                  RuleFor(x => x.Nickname)
                         .MinimumLength(5)
                         .MaximumLength(50);
            });
            RuleFor(x => x.DateOfBirth)
                    .GreaterThan(DateTime.MinValue)
                    .LessThan(DateTime.MaxValue);
        }
    }
}
