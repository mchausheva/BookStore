using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace BookStore.DL.Repositories.MsSQL
{
    public class BookMsSqlRepository : IBookRepository
    {
        private readonly ILogger<AuthorMsSqlRepository> _logger;
        private readonly IConfiguration _configuration;
        public BookMsSqlRepository(ILogger<AuthorMsSqlRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryAsync<Book>("SELECT * FROM Books WITH(NOLOCK)");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllBooks)} - {ex.Message}", ex);
            }
            return Enumerable.Empty<Book>();
        }

        public async Task<Book?> GetById(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<Book>("SELECT * FROM Books WITH(NOLOCK) WHERE Id = @Id", new { Id = id });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetById)} - {ex.Message}", ex);                
            }            
            return new Book();
        }

        public async Task<Book> GetByTitle(string title)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<Book>("SELECT * FROM Books WITH(NOLOCK) WHERE Title = @Title", new { Title = title});
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetByTitle)} - {ex.Message}", ex);
            }
            return new Book();
        }

        public async Task<Book> AddBook(Book book)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    if (!await BookAuthor(book.AuthorId)) return null;
                    else
                    {
                        var query = "INSERT INTO Books (Title, AuthorId, Quantity, LastUpdated, Price) VALUES (@Title, @AuthorId, @Quantity, @LastUpdated, @Price)";
                        var result = await conn.ExecuteAsync(query, book);
                        return book;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddBook)} - {ex.Message}", ex);
                return null;
            }            
        }

        public async Task<Book> UpdateBook(Book book)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var query = @"UPDATE Books SET Title = @Title, AuthorId = @AuthorId, Quantity = @Quantity, LastUpdated = @LastUpdated, Price = @Price WHERE Id = @Id";
                    var result = await conn.ExecuteAsync(query, book);

                    return book;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateBook)} - {ex.Message}", ex);
                return null;
            }
        }

        public async Task<Book?> DeleteBookById(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    return await conn.QueryFirstOrDefaultAsync<Book>("DELETE FROM Books WHERE Id = @Id", new { Id = id });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(DeleteBookById)} - {ex.Message}", ex);
                return null;
            }
        }

        public async Task<bool> BookAuthor (int authorId)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var author = await conn.QueryMultipleAsync("SELECT * FROM Authors WITH(NOLOCK) WHERE Id = @AuthorId", new { AuthorId = authorId });
                    if (author != null) return true;

                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(DeleteBookById)} - {ex.Message}", ex);
                return false;
            }
        }
    }
}
