using BookStore.Models.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;

namespace BookStore.BL.Interfaces
{
    public interface IAuthorService
    {
        IEnumerable<Author> GetAllAuthors();
        Author GetById(int id);
        Author GetAuthorByName(string name);
        Author GetAuthorByNickname(string nickname);
        AddAuthorResponse AddAuthor(AddAuthorRequest author);
        UpdateAuthorResponse UpdateAuthor(UpdateAuthorRequest authorRequest);
        Author DeleteAuthorById(int id);
    }
}
