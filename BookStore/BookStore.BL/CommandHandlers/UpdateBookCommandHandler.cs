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
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, UpdateBookResponse>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateBookCommandHandler> _logger;
        public UpdateBookCommandHandler(IBookRepository bookRepository, IMapper mapper, ILogger<UpdateBookCommandHandler> logger)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UpdateBookResponse> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var bookExist = await _bookRepository.GetById(request.updateBookRequest.Id);
                if (bookExist == null)
                {
                    return new UpdateBookResponse()
                    {
                        Book = bookExist,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "Not Updated"
                    };
                }

                var book = _mapper.Map<Book>(request.updateBookRequest);
                var result = await _bookRepository.UpdateBook(book);

                return new UpdateBookResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Book = result,
                    Message = "Successfully Updated Book"
                };
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"The book can not be update");
                throw;
            }
        }
    }
}
