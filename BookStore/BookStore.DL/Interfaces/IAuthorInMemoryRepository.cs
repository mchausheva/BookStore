using BookStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DL.Interfaces
{
    public interface IAuthorInMemoryRepository
    {
        IEnumerable<Author> GetAllAuthors();
        Author GetById(int id);
        Author? AddAuthor(Author author);
        Author? UpdateAuthor(Author author);
        Author? DeleteAuthorById(int id);
    }
}
