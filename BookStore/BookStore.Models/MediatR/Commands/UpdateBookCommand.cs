using BookStore.Models.Requests;
using BookStore.Models.Responses;
using MediatR;

namespace BookStore.Models.MediatR.Commands
{
    public record UpdateBookCommand(UpdateBookRequest bookRequest) : IRequest<UpdateBookResponse>
    {
        public readonly UpdateBookRequest updateBookRequest = bookRequest;
    }
}
