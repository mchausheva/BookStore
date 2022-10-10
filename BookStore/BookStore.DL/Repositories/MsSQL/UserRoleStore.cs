using BookStore.Models.Models.Users;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace BookStore.DL.Repositories.MsSQL
{
    public class UserRoleStore : IRoleStore<UserRole>
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserRoleStore> _logger;
        public UserRoleStore(IConfiguration configuration, ILogger<UserRoleStore> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<IdentityResult> CreateAsync(UserRole role, CancellationToken cancellationToken)
        {
            await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync(cancellationToken);

                    var query = @"INSERT INTO UserRoles ([Id], [RoleId], [UserId]) VALUES (@Id, @RoleId, @UserId)";
                    var result = await conn.ExecuteAsync(query, role);

                    return IdentityResult.Success;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error in {nameof(UserRoleStore.CreateAsync)} - {ex.Message}", ex);

                    return IdentityResult.Failed();
                }
            }
        }
        public async Task<UserRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync(cancellationToken);

                    return await conn.QueryFirstOrDefaultAsync<UserRole>(@"SELECT ur.Id, ur.RoleId, ur.UserId, r.RoleName as Name FROM UserRoles ur WITH (NOLOCK) 
                                                INNER JOIN Roles r ON ur.RoleId = r.Id
                                                WHERE r.RoleName = @Name", new { Name = normalizedRoleName });
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error in {nameof(UserRoleStore.FindByNameAsync)} - {ex.Message}", ex);
                    return null;
                }
            }
        }

        // ....... //

        public Task<IdentityResult> DeleteAsync(UserRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()  { }

        public Task<UserRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedRoleNameAsync(UserRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleIdAsync(UserRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleNameAsync(UserRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedRoleNameAsync(UserRole role, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetRoleNameAsync(UserRole role, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(UserRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
