using AutoMapper;
using BookStore.BL.Interfaces;
using BookStore.BL.Services;
using BookStore.DL.Interfaces;
using BookStore.Models.MediatR.Commands;
using BookStore.Models.Models;
using BookStore.Models.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace BookStore.BL.CommandHandlers
{
    public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, AddAuthorResponse>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddAuthorCommandHandler> _logger;

        public AddAuthorCommandHandler(IAuthorRepository authorRepository, IMapper mapper, ILogger<AddAuthorCommandHandler> logger)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AddAuthorResponse> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var auth = await _authorRepository.GetAuthorByName(request.addAuthorRequest.Name);
                if (auth != null)
                    return new AddAuthorResponse()
                    {
                        Author = auth,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "Author Already Exist!"
                    };

                var author = _mapper.Map<Author>(request.addAuthorRequest);
                var result = await _authorRepository.AddAuthor(author);

                return new AddAuthorResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Author = result,
                    Message = "Successfully Added Author"
                };
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"The author can not be add");
                throw;
            }
        }
    }
}
