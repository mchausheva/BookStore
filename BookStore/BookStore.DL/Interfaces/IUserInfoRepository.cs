using BookStore.Models.Models.Users;

namespace BookStore.DL.Interfaces
{
    public interface IUserInfoRepository
    {
        public Task<UserInfo?> GetUserInfoAsync(string email, string password);
    }
}
