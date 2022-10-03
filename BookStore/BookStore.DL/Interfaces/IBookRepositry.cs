using BookStore.Models.Models;

namespace BookStore.DL.Interfaces
{
    public interface IBookRepositry
    {
        IEnumerable<Book> GetAllBooks();
        Book GetById(int id);
        Book GetByTitle(string title);
        Book AddBook(Book book);
        Book UpdateBook(Book book);
        Book? DeleteBookById(int id);
    }
}
