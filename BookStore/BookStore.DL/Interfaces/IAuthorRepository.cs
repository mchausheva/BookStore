using BookStore.Models.Models;

namespace BookStore.DL.Interfaces
{
    public interface IAuthorRepository
    {
        IEnumerable<Author> GetAllAuthors();
        Author GetById(int id);
        Author GetAuthorByName(string name);
        Author GetAuthorByNickname(string nickname);
        Author AddAuthor(Author author);
        Author UpdateAuthor(Author author);
        Author DeleteAuthorById(int id);
    }
}
