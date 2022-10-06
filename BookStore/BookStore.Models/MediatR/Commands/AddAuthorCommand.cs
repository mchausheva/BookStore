using BookStore.Models.Requests;
using BookStore.Models.Responses;
using MediatR;

namespace BookStore.Models.MediatR.Commands
{
    public record AddAuthorCommand(AddAuthorRequest  authorRequest) : IRequest<AddAuthorResponse>
    {
        public readonly AddAuthorRequest addAuthorRequest = authorRequest;
    }
}
