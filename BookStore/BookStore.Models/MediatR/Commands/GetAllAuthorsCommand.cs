using BookStore.Models.Models;
using MediatR;

namespace BookStore.Models.MediatR.Commands
{
    public record GetAllAuthorsCommand : IRequest<IEnumerable<Author>>
    {
    }
}
