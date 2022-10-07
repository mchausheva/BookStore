using BookStore.Models.Requests;
using BookStore.Models.Responses;
using MediatR;

namespace BookStore.Models.MediatR.Commands
{
    public record AddBookCommand(AddBookRequest bookRequest) : IRequest<AddBookResponse>
    {
        public readonly AddBookRequest addBookRequest = bookRequest;
    }
}
