using BookStore.Models.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;

namespace BookStore.BL.Interfaces
{
    public interface IBookService
    {
        IEnumerable<Book> GetAllBooks();
        Book GetById(int id);
        AddBookResponse AddBook(AddBookRequest bookRequest);
        UpdateBookResponse UpdateBook(UpdateBookRequest bookRequest);
        Book DeleteBookById(int id);
    }
}
