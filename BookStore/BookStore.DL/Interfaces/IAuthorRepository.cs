using BookStore.Models.Models;

namespace BookStore.DL.Interfaces
{
    public interface IAuthorRepository
    {
        public Task<IEnumerable<Author>> GetAllAuthors();
        public Task<Author?> GetById(int id);
        public Task<Author> GetAuthorByName(string name);
        public Task<Author> GetAuthorByNickname(string nickname);
        public Task<Author> AddAuthor(Author author);
        public Task<Author> UpdateAuthor(Author author);
        public Task<Author?> DeleteAuthorById(int id);
        public Task<bool> AddMultipleAuthors(IEnumerable<Author> authorCollection);
        public Task<bool> AuthorBook(int authorId);
    }
}
