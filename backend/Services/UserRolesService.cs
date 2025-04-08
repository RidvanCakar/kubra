using backend.Models;
using Context;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class UserRolesService
    {
        private readonly eCommerceContext _context;

        public UserRolesService(eCommerceContext context)
        {
            _context = context;
        }

        public async Task<List<UserRolesDto>> GetUserRolesAsync()
        {
            var userRoles = await _context.UserRoles
            .Include(userRol => userRol.User)
            .Include(userRol => userRol.Role)
            .Select(userRol => new UserRolesDto
            {
                UserID = userRol.UserID,
                RoleID = userRol.RoleID,
                UserName = userRol.User.Username,
                RoleName = userRol.Role.RoleName
            }).ToListAsync();

            if (userRoles.Count > 0)
            {
                return userRoles;
            }
            else
            {
                return null;
            }
        }

        public async Task<UserRolesDto> GetUserRoleAsync(int userId)
        {
            var userRole = await _context.UserRoles
                .Where(userRol => userRol.UserID == userId)
                .Include(userRol => userRol.User)
                .Include(userRol => userRol.Role)
                .Select(userRol => new UserRolesDto
                {
                    UserID = userRol.UserID,
                    RoleID = userRol.RoleID,
                    UserName = userRol.User.Username,
                    RoleName = userRol.Role.RoleName
                }).FirstOrDefaultAsync();

            return userRole;
        }

        public async Task<UserRoles> AddUserRoleAsync(UserRoles userRole)
        {
            var lastUserRole = await _context.UserRoles.OrderByDescending(u => u.ID).FirstOrDefaultAsync();
            if (lastUserRole != null)
            {
                userRole.ID = lastUserRole.ID + 1;
            }
            else
            {
                userRole.ID = 1;
            }
            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();
            return userRole;
        }

        public async Task<UserRoles> UpdateUserRoleAsync(int userId, UserRoles updateUserRole)
        {
            if (userId != updateUserRole.UserID)
                return null;

            var userRole = await _context.UserRoles.FindAsync(userId);
            if (userRole == null)
                return null;

            userRole.RoleID = updateUserRole.RoleID;
            await _context.SaveChangesAsync();
            return userRole;
        }

    }
}