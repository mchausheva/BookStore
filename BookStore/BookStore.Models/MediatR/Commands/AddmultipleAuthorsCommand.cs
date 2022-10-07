using BookStore.Models.Models;
using MediatR;

namespace BookStore.Models.MediatR.Commands
{
    public record AddmultipleAuthorsCommand (IEnumerable<Author> addAuthorCollection) : IRequest<bool>
    {
    }
}
