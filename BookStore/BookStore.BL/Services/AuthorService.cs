using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BL.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorInMemoryRepository _authorRepository;
        public AuthorService(IAuthorInMemoryRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public Author? AddAuthor(Author author)
        {
            return _authorRepository.AddAuthor(author);
        }

        public Author? DeleteAuthorById(int id)
        {
            return _authorRepository.DeleteAuthorById(id);
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            return _authorRepository.GetAllAuthors();
        }

        public Author GetById(int id)
        {
            return _authorRepository.GetById(id);   
        }

        public Author? UpdateAuthor(Author author)
        {
            return _authorRepository.UpdateAuthor(author);
        }
    }
}
