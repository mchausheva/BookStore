using BookStore.Models.Requests;
using BookStore.Models.Responses;
using MediatR;

namespace BookStore.Models.MediatR.Commands
{
    public record UpdateAuthorCommand(UpdateAuthorRequest authorRequest) : IRequest<UpdateAuthorResponse>
    {
        public readonly UpdateAuthorRequest updateAuthorRequest = authorRequest;
    }
}
