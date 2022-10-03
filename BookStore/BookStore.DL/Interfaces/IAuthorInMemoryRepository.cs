using BookStore.Models.Models;

namespace BookStore.DL.Interfaces
{
    public interface IAuthorInMemoryRepository
    {
        IEnumerable<Author> GetAllAuthors();
        Author GetById(int id);
        Author? AddAuthor(Author author);
        Author? UpdateAuthor(Author author);
        Author? DeleteAuthorById(int id);
    }
}
