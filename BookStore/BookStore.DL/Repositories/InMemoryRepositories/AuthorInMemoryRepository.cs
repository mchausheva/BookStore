using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using Microsoft.Extensions.Logging;

namespace BookStore.DL.Repositories.InMemoryRepositories
{
    public class AuthorInMemoryRepository : IAuthorRepository
    {
        private readonly ILogger<AuthorInMemoryRepository> _logger;
        private static List<Author> _authors = new List<Author>()
        {
            new Author()
            {
                Id = 1,
                Name = "Gosho",
                Age = 22,
                Nickname = "sho"
            },
            new Author()
            {
                Id = 2,
                Name = "Pesho",
                Age = 33,
                Nickname = "PEPI"
            }
        };
        public AuthorInMemoryRepository(ILogger<AuthorInMemoryRepository> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            return _authors;
        }
        public Author? GetById(int id)
        {
            return _authors.FirstOrDefault(a => a.Id == id);
        }
        public Author AddAuthor(Author author)
        {
            _authors.Add(author);
            return author;
        }
        public Author UpdateAuthor(Author author)
        {
            var existingAuthor = _authors.FirstOrDefault(a => a.Id == author.Id);
            if (existingAuthor == null) return null;
            _authors.Remove(existingAuthor);
            _authors.Add(author);
            return author;
        }
        public Author? DeleteAuthorById(int id)
        {
            if (id <= 0) return null;
            var author = _authors.FirstOrDefault(u => u.Id == id);
            _authors.Remove(author);
            return author;
        }
        public Author GetAuthorByName(string name)
        {
            return _authors.FirstOrDefault(a => a.Name == name);
        }

        public Author GetAuthorByNickname(string nickname)
        {
            return _authors.FirstOrDefault(a => a.Nickname == nickname);
        }

        public bool AddMultipleAuthors(IEnumerable<Author> authorCollection)
        {
            try
            {
                _authors.AddRange(authorCollection);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Unable to add multiple authors with message: {ex.Message}");
                return false;
            }
        }
    }
}
