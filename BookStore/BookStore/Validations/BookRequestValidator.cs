using BookStore.Models.Requests;
using FluentValidation;

namespace BookStore.Validations
{
    public class BookRequestValidator : AbstractValidator<AddBookRequest>
    {
        public BookRequestValidator()
        {
            RuleFor(x => x.Title).NotEmpty()
                                .MinimumLength(3)
                                .MaximumLength(60);
            RuleFor(x => x.AuthorId).NotEmpty();
            RuleFor(x => x.Quantity).NotEmpty();
        }
    }
}
