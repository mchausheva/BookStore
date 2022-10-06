using BookStore.DL.Interfaces;
using BookStore.Models.MediatR.Commands;
using BookStore.Models.Models;
using MediatR;

namespace BookStore.BL.CommandHandlers
{
    public record GetAllBooksCommandHandler : IRequestHandler<GetAllBooksCommand, IEnumerable<Book>>
    {
        private readonly IBookRepository _bookRepositry;

        public GetAllBooksCommandHandler(IBookRepository bookRepositry)
        {
            _bookRepositry = bookRepositry;
        }

        public async Task<IEnumerable<Book>> Handle(GetAllBooksCommand request, CancellationToken cancellationToken)
        {
            return await _bookRepositry.GetAllBooks();
        }
    }
}
