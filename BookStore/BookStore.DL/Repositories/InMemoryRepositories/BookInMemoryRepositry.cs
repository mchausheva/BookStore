using BookStore.DL.Interfaces;
using BookStore.Models.Models;

namespace BookStore.DL.Repositories.InMemoryRepositories
{
    public class BookInMemoryRepositry : IBookInMemoryRepositry
    {
        private static List<Book> _books = new List<Book>()
        {
            new Book()
            {
                Id = 1,
                Title = "Harry Potter",
                AuthorId = 1
            },
            new Book()
            {
                Id = 2,
                Title = "Peter Pan",
                AuthorId = 2
            }
        };
        public Book? AddBook(Book book)
        {
            _books.Add(book);
            return book;
        }

        public Book? DeleteBookById(int id)
        {
            if (id <= 0) return null;
            var book = _books.FirstOrDefault(u => u.Id == id);
            _books.Remove(book);
            return book;
        }

        public IEnumerable<Book> GetAllBook()
        {
            return _books;
        }

        public Book GetById(int id)
        {
            return _books.FirstOrDefault(b => b.Id == id);
        }

        public Book? UpdateBook(Book book)
        {
            var existingBook = _books.FirstOrDefault(a => a.Id == book.Id);
            if (existingBook == null) return null;
            _books.Remove(existingBook);
            _books.Add(book);
            return book;
        }
    }
}
