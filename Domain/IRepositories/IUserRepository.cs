using Domain.Entities;

namespace Domain.IRepositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByUsername(string username);
        Task<User> GetUserByPhoneNumber(string phoneNumber);
        Task AddRolesToUserAsync(User user, IList<string> roles);
        Task<IEnumerable<string>> GetRolesOfUserAsync(User user);
        Task DeleteRolesAsync(User user, List<string> roles);
    }
}
