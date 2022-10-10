using BookStore.BL.Interfaces;
using BookStore.Models.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace BookStore.BL.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<UserInfo> _userManager;
        private readonly IPasswordHasher<UserInfo> _passwordHasher;
        private readonly RoleManager<UserRole> _roleManager;
        public IdentityService(UserManager<UserInfo> userManager,IPasswordHasher<UserInfo> passwordHasher, RoleManager<UserRole> roleManager)
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> CreateAsync(UserInfo user)
        {
            var exUser = await _userManager.GetUserIdAsync(user);

            if (string.IsNullOrEmpty(exUser))
            {
                return await _userManager.CreateAsync(user);
            }
            return IdentityResult.Failed();
        }

        public async Task<UserInfo?> CheckUserAndPass(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null) return null;

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);

            return result == PasswordVerificationResult.Success ? user : null;
        }

        public async Task<IEnumerable<string>> GetUserRoles(UserInfo user)
        {
            return await _userManager.GetRolesAsync(user);
        }
    }
}
