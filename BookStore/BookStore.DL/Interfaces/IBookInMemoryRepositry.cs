using BookStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
