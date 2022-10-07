using BookStore.DL.Interfaces;
using BookStore.Models.MediatR.Commands;
using BookStore.Models.Models;
using MediatR;

namespace BookStore.BL.CommandHandlers
{
    public class GetAuthorByIdCommandHandler : IRequestHandler<GetAuthorByIdCommand, Author>
    {
        private readonly IAuthorRepository _authorRepository;
        public GetAuthorByIdCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Author> Handle(GetAuthorByIdCommand request, CancellationToken cancellationToken)
        {
            return await _authorRepository.GetById(request.id);
        }
    }
}
