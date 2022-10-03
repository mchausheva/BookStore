using BookStore.Models.Models;

namespace BookStore.BL.Interfaces
{
    public interface IAuthorService
    {
        IEnumerable<Author> GetAllAuthors();
        Author GetById(int id);
        Author? AddAuthor(Author author);
        Author? UpdateAuthor(Author author);
        Author? DeleteAuthorById(int id);
    }
}
