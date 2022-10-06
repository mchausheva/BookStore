using AutoMapper;
using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.MediatR.Commands;
using BookStore.Models.Models;
using BookStore.Models.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace BookStore.BL.CommandHandlers
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, AddBookResponse>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddBookCommandHandler> _logger;
        public AddBookCommandHandler(IBookRepository bookService, IMapper mapper, ILogger<AddBookCommandHandler> logger)
        {
            _bookRepository = bookService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AddBookResponse> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var bookExist = await _bookRepository.GetById(request.addBookRequest.Id);
                if (bookExist != null)
                    return new AddBookResponse()
                    {
                        Book = bookExist,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "This Book Already Exist!"
                    };

                var book = _mapper.Map<Book>(request.addBookRequest);
                var result = await _bookRepository.AddBook(book);

                return new AddBookResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Book = result,
                    Message = "Successfully Added Book"
                };
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"The book can not be add");
                throw;
            }
        }
    }
}
