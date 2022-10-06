using BookStore.Models.Models;
using MediatR;

namespace BookStore.Models.MediatR.Commands
{
    public record DeleteBookCommand(int bookId) : IRequest<Book>
    {
    }
}
