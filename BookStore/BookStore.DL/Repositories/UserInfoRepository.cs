using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.Users;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace BookStore.DL.Repositories
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private readonly ILogger<UserInfoRepository> _logger;
        private readonly IConfiguration _configuration;
        public UserInfoRepository(ILogger<UserInfoRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<UserInfo?> GetUserInfoAsync(string email, string password)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<UserInfo>("SELECT * FROM UserInfo WITH(NOLOCK) WHERE Email = @Email AND Password = @Password",
                                                                        new {Email = email, Password = password});
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetUserInfoAsync)} - {ex.Message}", ex);
            }
            return new UserInfo();
        }
    }
}
