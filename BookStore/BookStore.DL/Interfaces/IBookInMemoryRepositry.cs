using BookStore.Models.Models;

namespace BookStore.DL.Interfaces
{
    public interface IBookInMemoryRepositry
    {
        IEnumerable<Book> GetAllBook();
        Book GetById(int id);
        Book? AddBook(Book book);
        Book? UpdateBook(Book book);
        Book? DeleteBookById(int id);
    }
}
