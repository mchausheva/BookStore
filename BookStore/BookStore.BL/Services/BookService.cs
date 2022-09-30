using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BL.Services
{
    public class BookService : IBookService
    {
        private readonly IBookInMemoryRepositry _bookRepository;
        public BookService(IBookInMemoryRepositry bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public Book? AddBook(Book book)
        {
            return _bookRepository.AddBook(book);
        }

        public Book? DeleteBookById(int id)
        {
            return _bookRepository.DeleteBookById(id);
        }

        public IEnumerable<Book> GetAllBook()
        {
            return _bookRepository.GetAllBook();
        }

        public Book GetById(int id)
        {
            return _bookRepository.GetById(id);
        }

        public Book? UpdateBook(Book book)
        {
            return _bookRepository.UpdateBook(book);
        }
    }
}
