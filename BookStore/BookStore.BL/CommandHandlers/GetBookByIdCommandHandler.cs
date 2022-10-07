using BookStore.DL.Interfaces;
using BookStore.Models.MediatR.Commands;
using BookStore.Models.Models;
using MediatR;

namespace BookStore.BL.CommandHandlers
{
    public class GetBookByIdCommandHandler : IRequestHandler<GetBookByIdCommand, Book>
    {
        private readonly IBookRepository _bookRepositry;

        public GetBookByIdCommandHandler(IBookRepository bookRepositry)
        {
            _bookRepositry = bookRepositry;
        }

        public async Task<Book> Handle(GetBookByIdCommand request, CancellationToken cancellationToken)
        {
            return await _bookRepositry.GetById(request.id);
        }
    }
}
