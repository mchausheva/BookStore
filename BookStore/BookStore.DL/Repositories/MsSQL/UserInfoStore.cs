using BookStore.Models.Models.Users;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace BookStore.DL.Repositories.MsSQL
{
    public class UserInfoStore : IUserPasswordStore<UserInfo>, IUserRoleStore<UserInfo>
    {
        private readonly ILogger<UserInfoStore> _logger;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<UserInfo> _passwordHasher;

        public UserInfoStore(ILogger<UserInfoStore> logger, IConfiguration configuration, IPasswordHasher<UserInfo> passwordHasher)
        {
            _logger = logger;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
        }

        public async Task<string> GetUserIdAsync(UserInfo user, CancellationToken cancellationToken)
        {
            await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync(cancellationToken);

                    var result = await conn.QueryFirstOrDefaultAsync<UserInfo>("SELECT * FROM UserInfo WITH(NOLOCK) WHERE UserId = @UserId", new { UserId = user.UserId });

                    return result?.UserId.ToString();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error in {nameof(GetUserIdAsync)} - {ex.Message}", ex);
                    return null;    
                }
            }
        }
        
        public async Task<string> GetUserNameAsync(UserInfo user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.UserName);
        }
       
        public async Task<IdentityResult> CreateAsync(UserInfo user, CancellationToken cancellationToken)
        {
            await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync(cancellationToken);

                    var query = @"INSERT INTO UserInfo (DisplayName, UserName, Email, Password, CreatedDate)
                                                VALUES (@DisplayName, @UserName, @Email, @Password, @CreatedDate)";

                    user.Password = _passwordHasher.HashPassword(user, user.Password);

                    var result = await conn.ExecuteAsync(query, user);

                    return result > 0 ? IdentityResult.Success : IdentityResult.Failed();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error in {nameof(CreateAsync)} - {ex.Message}", ex);

                    return IdentityResult.Failed(new IdentityError()
                    {
                        Description = "error"
                    });
                }
            }
        }
        
        public async Task<UserInfo> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync(cancellationToken);

                    return await conn.QueryFirstOrDefaultAsync<UserInfo>("SELECT * FROM UserInfo WITH(NOLOCK) WHERE UserId = @UserId",
                                                                        new { UserId = userId });
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error in {nameof(FindByIdAsync)} - {ex.Message}", ex);
                }
                return new UserInfo();
            }
        }
        
        public async Task<UserInfo> FindByNameAsync(string normalizedName, CancellationToken cancellationToken)
        {
            await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync(cancellationToken);

                    return await conn.QueryFirstOrDefaultAsync<UserInfo>("SELECT * FROM UserInfo WITH(NOLOCK) WHERE UserName = @UserName",
                                                                        new { UserName = normalizedName });
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error in {nameof(FindByNameAsync)} - {ex.Message}", ex);
                }
                return new UserInfo();
            }
        }
        
        public Task SetNormalizedUserNameAsync(UserInfo user, string normalizedName, CancellationToken cancellationToken)
        {
            user.UserName = normalizedName;
            return Task.CompletedTask;
        }

        public void Dispose() {  }

        public async Task SetPasswordHashAsync(UserInfo user, string passwordHash, CancellationToken cancellationToken)
        {
            await using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            await conn.OpenAsync(cancellationToken);

            await conn.ExecuteAsync("UPDATE UserInfo SET Password = @passwordHash WHERE UserId = @userId", new { user.UserId, passwordHash });
        }
        
        public async Task<string> GetPasswordHashAsync(UserInfo user, CancellationToken cancellationToken)
        {
            await using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            await conn.OpenAsync(cancellationToken);

            return await conn.QueryFirstOrDefaultAsync<string>("SELECT * FROM UserInfo WITH(NOLOCK) WHERE UserId = @userId", new { user.UserId });
        }
        
        public async Task<bool> HasPasswordAsync(UserInfo user, CancellationToken cancellationToken)
        {
            return !string.IsNullOrEmpty(await GetPasswordHashAsync(user, cancellationToken));
        }

        // ........ //

        public Task<IdentityResult> DeleteAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(UserInfo user, CancellationToken cancellationToken)
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

        public Task AddToRoleAsync(UserInfo user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(UserInfo user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<string>> GetRolesAsync(UserInfo user, CancellationToken cancellationToken)
        {
            await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync(cancellationToken);

                    var result =
                        await conn.QueryAsync<string>(@"SELECT r.RoleName FROM Roles r WHERE r.Id IN 
                                                       (SELECT ur.Id FROM UserRoles ur WHERE ur.UserId = @UserId )", new { UserId = user.UserId });

                    return result.ToList();
                }
                catch (Exception e)
                {
                    _logger.LogError($"Error in {nameof(UserRoleStore.FindByNameAsync)}:{e.Message}");
                    return null;
                }
            }
        }

        public Task<bool> IsInRoleAsync(UserInfo user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UserInfo>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
