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

        public async Task<AddAuthorResponse> AddAuthor(AddAuthorRequest authorRequest)
        {
            try
            {
                var auth = await _authorRepository.GetById(authorRequest.Id);
                if (auth != null)
                    return new AddAuthorResponse()
                    {
                        Author = auth,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "Author Already Exist!"
                    };

                var author = _mapper.Map<Author>(authorRequest);
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

        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            _logger.LogInformation("Success");
            return await _authorRepository.GetAllAuthors();
        }

        public async Task<Author> GetAuthorByName(string name)
        {
            _logger.LogInformation("Success");
            return await _authorRepository.GetAuthorByName(name);
        }

        public async Task<Author> GetAuthorByNickname(string nickname)
        {
            _logger.LogInformation("Success");
            return await _authorRepository.GetAuthorByNickname(nickname);
        }

        public async Task<Author> GetById(int id)
        {
            _logger.LogInformation("Success");
            return await _authorRepository.GetById(id);
        }

        public async Task<UpdateAuthorResponse> UpdateAuthor (UpdateAuthorRequest authorRequest)
        {
            try
            {
                var auth = await _authorRepository.GetById(authorRequest.Id);
                if (auth == null)
                {
                    return new UpdateAuthorResponse()
                    {
                        Author = auth,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "Not Updated"
                    };
                }

                var author = _mapper.Map<Author>(authorRequest);
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

        public async Task<Author> DeleteAuthorById(int id)
        {
            _logger.LogInformation("Success");
            return await _authorRepository.DeleteAuthorById(id);
        }

        public async Task<bool> AddMultipleAuthors(IEnumerable<Author> authorCollection)
        {
            return await _authorRepository.AddMultipleAuthors(authorCollection);
        }
    }
}
