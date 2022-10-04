using BookStore.Models.Requests;
using FluentValidation;

namespace BookStore.Validations
{
    public class MultipleAuthorsRequestValidator : AbstractValidator<AddMultipleAuthorsRequest>
    {
        public MultipleAuthorsRequestValidator()
        {
            RuleFor(x => x.AuthorRequests).NotEmpty();
            RuleFor(x => x.Reason).MaximumLength(100);
        }
    }
}
