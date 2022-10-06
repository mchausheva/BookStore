using AutoMapper;
using BookStore.DL.Interfaces;
using BookStore.Models.MediatR.Commands;
using BookStore.Models.Models;
using BookStore.Models.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace BookStore.BL.CommandHandlers
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, UpdateAuthorResponse>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateAuthorCommandHandler> _logger;
public UpdateAuthorCommandHandler(IAuthorRepository authorRepository, IMapper mapper, ILogger<UpdateAuthorCommandHandler> logger)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UpdateAuthorResponse> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var auth = await _authorRepository.GetAuthorByName(request.updateAuthorRequest.Name);
                if (auth == null)
                {
                    return new UpdateAuthorResponse()
                    {
                        Author = auth,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "Not Updated"
                    };
                }

                var author = _mapper.Map<Author>(request.updateAuthorRequest);
                var result = await _authorRepository.UpdateAuthor(author);

                return new UpdateAuthorResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Author = result,
                    Message = "Successfully Updated Author"
                };
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"The author can not be update");
                throw;
            }
        }
    }
}
