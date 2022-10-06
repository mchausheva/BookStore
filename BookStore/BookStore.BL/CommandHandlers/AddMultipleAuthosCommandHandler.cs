using BookStore.DL.Interfaces;
using BookStore.Models.MediatR.Commands;
using MediatR;

namespace BookStore.BL.CommandHandlers
{
    public class AddMultipleAuthosCommandHandler : IRequestHandler<AddmultipleAuthorsCommand, bool>
    {
        private readonly IAuthorRepository _authorRepository;
        public AddMultipleAuthosCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<bool> Handle(AddmultipleAuthorsCommand request, CancellationToken cancellationToken)
        {
            return await _authorRepository.AddMultipleAuthors(request.addAuthorCollection);
        }
    }
}
