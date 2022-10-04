using AutoMapper;
using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;

namespace BookStore.BL.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorService> _logger;
        public AuthorService(IAuthorRepository authorRepository, IMapper mapper, ILogger<AuthorService> logger)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public AddAuthorResponse AddAuthor(AddAuthorRequest authorRequest)
        {
            try
            {
                var auth = _authorRepository.GetAuthorByName(authorRequest.Name);
                if (auth != null)
                    return new AddAuthorResponse()
                    {
                        Author = auth,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "Author Already Exist!"
                    };

                var author = _mapper.Map<Author>(authorRequest);
                var result = _authorRepository.AddAuthor(author);

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

        public Author DeleteAuthorById(int id)
        {
            _logger.LogInformation("Success");
            return _authorRepository.DeleteAuthorById(id);
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            _logger.LogInformation("Success");
            return _authorRepository.GetAllAuthors();
        }

        public Author GetAuthorByName(string name)
        {
            _logger.LogInformation("Success");
            return _authorRepository.GetAuthorByName(name);
        }

        public Author GetAuthorByNickname(string nickname)
        {
            _logger.LogInformation("Success");
            return _authorRepository.GetAuthorByNickname(nickname);
        }

        public Author GetById(int id)
        {
            _logger.LogInformation("Success");
            return _authorRepository.GetById(id);
        }

        public UpdateAuthorResponse UpdateAuthor(UpdateAuthorRequest authorRequest)
        {
            try
            {

                var auth = _authorRepository.GetAuthorByName(authorRequest.Name);
                if (auth == null)
                    return new UpdateAuthorResponse()
                    {
                        Author = auth,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "Not Updated"
                    };

                var author = _mapper.Map<Author>(auth);
                var result = _authorRepository.UpdateAuthor(author);

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

        public bool AddMultipleAuthors(IEnumerable<Author> authorCollection)
        {
            return _authorRepository.AddMultipleAuthors(authorCollection);
        }
    }
}
