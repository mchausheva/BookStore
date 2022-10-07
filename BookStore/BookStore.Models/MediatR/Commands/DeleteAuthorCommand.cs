using BookStore.Models.Models;
using MediatR;

namespace BookStore.Models.MediatR.Commands
{
    public record DeleteAuthorCommand(int authorId) : IRequest<Author>
    {
        public int AuthorId = authorId;
    }
}
