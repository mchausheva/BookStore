using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Reflection;

namespace BookStore.DL.Repositories.MsSQL
{
    public class AuthorMsSqlRepository : IAuthorRepository
    {
        private readonly ILogger<AuthorMsSqlRepository> _logger;
        private readonly IConfiguration _configuration;

        public AuthorMsSqlRepository(ILogger<AuthorMsSqlRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryAsync<Author>("SELECT * FROM Authors WITH(NOLOCK)");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllAuthors)} - {ex.Message}", ex);
            }
            return Enumerable.Empty<Author>();
        }

        public async Task<Author> GetById(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<Author>("SELECT * FROM Authors WITH(NOLOCK) WHERE Id = @Id", new { Id = id});
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetById)} - {ex.Message}", ex);
            }
            return new Author();
        }

        public async Task<Author> GetAuthorByName(string name)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<Author>("SELECT * FROM Authors WITH(NOLOCK) WHERE Name = @Name", new { Name = name });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAuthorByName)} - {ex.Message}", ex);
            }
            return new Author();
        }

        public async Task<Author> GetAuthorByNickname(string nickname)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<Author>("SELECT * FROM Authors WITH(NOLOCK) WHERE Nickname = @Nickname", new { Nickname = nickname });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAuthorByNickname)} - {ex.Message}", ex);
            }
            return new Author();
        }

        public async Task<Author> AddAuthor(Author author)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var query = "INSERT INTO Authors (Name,Age,DateOfBirth,NickName) VALUES (@Name, @Age, @DateOfBirth, @NickName)";
                    var result = await conn.ExecuteAsync(query, author);
                    return author;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddAuthor)} - {ex.Message}", ex);
            }
            return null;
        }

        public async Task<bool> AddMultipleAuthors(IEnumerable<Author> authorCollection)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var result = await conn.ExecuteAsync("INSERT INTO Authors (Name,Age,DateOfBirth,NickName) VALUES (@Name, @Age,  @DateOfBirth,  @NickName)"
                        , authorCollection);
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddMultipleAuthors)} - {ex.Message}", ex);
            }
            return false;
        }
        public async Task<Author> UpdateAuthor(Author author)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();             
                   

                    var query = "UPDATE Authors SET Name = @Name, Age = @Age, DateOfBirth = @DateOfBirth, Nickname = @Nickname WHERE Id = @Id";
                    var result = await conn.ExecuteAsync(query, author);

                    return author;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateAuthor)} - {ex.Message}", ex);
            }
            return null;
        }

        public async Task<Author> DeleteAuthorById(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    return await conn.QueryFirstOrDefaultAsync<Author>("DELETE FROM Authors WHERE Id = @Id", new { Id = id });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(DeleteAuthorById)} - {ex.Message}", ex);
            }
            return null;
        }
    }
}
