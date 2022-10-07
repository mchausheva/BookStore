using BookStore.DL.Interfaces;
using BookStore.Models.MediatR.Commands;
using BookStore.Models.Models;
using MediatR;

namespace BookStore.BL.CommandHandlers
{
    public class GetAllAuthorsCommandHandler : IRequestHandler<GetAllAuthorsCommand, IEnumerable<Author>>
    {
        private readonly IAuthorRepository _authorRepository;
        public GetAllAuthorsCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<Author>> Handle(GetAllAuthorsCommand request, CancellationToken cancellationToken)
        {
            return await _authorRepository.GetAllAuthors();
        }
    }
}
