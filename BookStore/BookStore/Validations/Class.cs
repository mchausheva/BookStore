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
        }
    }
}
