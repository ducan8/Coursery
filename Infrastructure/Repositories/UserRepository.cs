using Domain.Entities;
using Domain.IRepositories;
using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }



        public async Task AddRolesToUserAsync(User user, IList<string> roles)
        {
            if (user == null) { throw new ArgumentNullException(nameof(user)); }
            if (roles == null) { throw new ArgumentNullException(nameof(roles)); }

            var listRoleUserHave = await GetRolesOfUserAsync(user);

            foreach (var role in roles)
            {
                if (await IsStringInListAsync(role, listRoleUserHave.ToList()))
                {
                    throw new ArgumentException("User have the role already");
                }
                else
                {
                    var roleItem = await _context.Roles.SingleOrDefaultAsync(x => x.RoleCode.Equals(role));
                    if (roleItem == null) throw new ArgumentException("The role does not exists");
                    _context.Permissions.Add(new Permission { RoleId = roleItem.Id, UserId = user.Id });
                }
            }
            await _context.SaveChangesAsync();

        }

        public async Task DeleteRolesAsync(User user, List<string> roles)
        {
            var listRoles = await GetRolesOfUserAsync(user);
            if (roles == null) throw new ArgumentNullException(nameof(roles));
            if (listRoles == null) throw new ArgumentNullException(nameof(listRoles));

            foreach (var role in listRoles)
            {
                foreach (var roleItem in roles)
                {
                    var roleObject = await _context.Roles.SingleOrDefaultAsync(x => x.RoleCode.Equals(roleItem));
                    var permissions = await _context.Permissions.SingleOrDefaultAsync(x => x.RoleId == roleObject.Id && x.UserId == user.Id);
                    if (await CompareStringAsync(role, roleItem))
                    {
                        _context.Permissions.Remove(permissions);
                    }
                }
            }
            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<string>> GetRolesOfUserAsync(User user)
        {
            var listPermissions = await _context.Permissions.Include(x => x.Role).Where(x => x.UserId == user.Id).ToListAsync();
            IList<string> roles = new List<string>();

            foreach (var permission in listPermissions.Distinct())
            {
                roles.Add(permission.Role!.RoleCode.ToString());
            }
            return roles.AsEnumerable();

        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));
            if (user == null) return null;
            return user;
        }

        public async Task<User> GetUserByPhoneNumber(string phoneNumber)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.PhoneNumber.ToLower().Equals(phoneNumber.ToLower()));
            if (user == null) return null;
            return user;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName.ToLower().Equals(username.ToLower()));
            if (user == null) return null;
            return user;
        }

        public async Task<IEnumerable<Certificate>> GetCertificatesAsync(Guid userId)
        {
            var user = await _context.Users.Include(x => x.Certificates).FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null) return null;
            if(user.Certificates.Any()) return user.Certificates;
            return null;
        }

        #region private method
        private Task<bool> CompareStringAsync(string str1, string str2)
        {
            return Task.FromResult(string.Equals(str1.ToLowerInvariant(), str2.ToLowerInvariant()));
        }

        private async Task<bool> IsStringInListAsync(string inputString, List<string> listString)
        {
            if (inputString == null)
            {
                throw new ArgumentNullException(nameof(inputString));
            }
            if (listString == null)
            {
                throw new ArgumentNullException(nameof(listString));
            }
            foreach (var item in listString)
            {
                if (await CompareStringAsync(inputString, item)) return true;
            }
            return false;
        }
        #endregion
    }
}
