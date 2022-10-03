using BookStore.Models.Models;

namespace BookStore.BL.Interfaces
{
    public interface IBookService
    {
        IEnumerable<Book> GetAllBook();
        Book GetById(int id);
        Book? AddBook(Book book);
        Book? UpdateBook(Book book);
        Book? DeleteBookById(int id);
    }
}
