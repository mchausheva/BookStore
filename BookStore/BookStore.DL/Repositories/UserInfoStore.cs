using BookStore.Models.Models.Users;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace BookStore.DL.Repositories
{
    public class UserInfoStore : IUserPasswordStore<UserInfo>
    {
        private readonly ILogger<UserInfoRepository> _logger;
        private readonly IConfiguration _configuration;

        public UserInfoStore(ILogger<UserInfoRepository> logger, IConfiguration configuration)
        {
            _logger = logger;   
            _configuration = configuration;
        }

        public async Task<string> GetUserIdAsync(UserInfo user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.UserId.ToString());
        }
        public async Task<string> GetUserNameAsync(UserInfo user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.UserName);
        }
        public async Task<IdentityResult> CreateAsync(UserInfo user, CancellationToken cancellationToken)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var query = "INSERT INTO UserInfo (UserId, DisplayName, UserName, Email, Password, CreatedDate) VALUES (@UserId, @DisplayName, @UserName, @Email, @Password, @CreatedDate)";

                    return IdentityResult.Success;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(FindByIdAsync)} - {ex.Message}", ex);
                return IdentityResult.Failed(new IdentityError()
                {
                    Description = "error"
                });
            }           
        }
        public async Task<UserInfo> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<UserInfo>("SELECT * FROM UserInfo WITH(NOLOCK) WHERE UserId = @UserId",
                                                                        new { UserId = userId });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(FindByIdAsync)} - {ex.Message}", ex);
            }
            return new UserInfo();
        }
        public async Task<UserInfo> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<UserInfo>("SELECT * FROM UserInfo WITH(NOLOCK) WHERE UserName = @UserName",
                                                                        new { UserName = normalizedUserName });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(FindByNameAsync)} - {ex.Message}", ex);
            }
            return new UserInfo();
        }
        public Task SetNormalizedUserNameAsync(UserInfo user, string normalizedName, CancellationToken cancellationToken)
        {
            user.UserName = normalizedName;
            return Task.CompletedTask;
        }

        // ........ //

        public Task SetPasswordHashAsync(UserInfo user, string passwordHash, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public Task<string> GetPasswordHashAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetUserNameAsync(UserInfo user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
