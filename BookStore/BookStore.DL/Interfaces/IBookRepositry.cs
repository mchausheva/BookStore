using BookStore.Models.Models;

namespace BookStore.DL.Interfaces
{
    public interface IBookRepositry
    {
        public Task<IEnumerable<Book>> GetAllBooks();
        public Task<Book> GetById(int id);
        public Task<Book> GetByTitle(string title);
        public Task<Book> AddBook(Book book);
        public Task<Book> UpdateBook(Book book);
        public Task<Book> DeleteBookById(int id);
        public Task<bool> BookAuthor(int authorId);
    }
}
