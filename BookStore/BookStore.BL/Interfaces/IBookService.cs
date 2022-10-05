using BookStore.Models.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;

namespace BookStore.BL.Interfaces
{
    public interface IBookService
    {
        public Task<IEnumerable<Book>> GetAllBooks();
        public Task<Book> GetById(int id);
        public Task<AddBookResponse> AddBook(AddBookRequest bookRequest);
        public Task<UpdateBookResponse> UpdateBook(UpdateBookRequest bookRequest);
        public Task<Book> DeleteBookById(int id);
    }
}
