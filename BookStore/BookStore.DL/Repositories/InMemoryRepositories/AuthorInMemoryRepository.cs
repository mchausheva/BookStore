using BookStore.DL.Interfaces;
using BookStore.Models.Models;

namespace BookStore.DL.Repositories.InMemoryRepositories
{
    public class AuthorInMemoryRepository : IAuthorRepository
    {
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
    }
}
