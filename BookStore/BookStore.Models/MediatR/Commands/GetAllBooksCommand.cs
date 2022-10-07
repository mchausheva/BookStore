using BookStore.Models.Models;
using MediatR;

namespace BookStore.Models.MediatR.Commands
{
    public record GetAllBooksCommand : IRequest<IEnumerable<Book>> 
    {
    }
}
