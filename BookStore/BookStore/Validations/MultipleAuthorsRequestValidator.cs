using BookStore.Models.Requests;
using FluentValidation;

namespace BookStore.Validations
{
    public class MultipleAuthorsRequestValidator : AbstractValidator<AddMultipleAuthorsRequest>
    {
        public MultipleAuthorsRequestValidator()
        {
            RuleFor(x => x.AuthorRequests).NotEmpty()
                                          .Must(x => x.Count() >= 2).WithMessage("There must be at least 2 authors.");
            RuleFor(x => x.Reason).MaximumLength(150);
        }
    }
}
