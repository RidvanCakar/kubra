using Entities;
using Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Services{
    public class RolesService
    {
        private readonly eCommerceContext _context;

        public RolesService (eCommerceContext context){
            _context= context;
        }

        public async Task<List<Roles>> GetRolesAsync(){
            var roles = await _context.Roles.ToListAsync();
                        return roles?.Any() == true ? roles : null;

        }

        public async Task<Roles> GetRoleAsync(int id){
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
                return null;
            return role;
        }

        public async Task<Roles> AddRoleAsync(Roles role){
           var lastRole = await _context.Roles.OrderByDescending(r => r.ID).FirstOrDefaultAsync();
            if (lastRole != null){
                role.ID = lastRole.ID + 1;
            }else{
                role.ID = 1;
            }
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return lastRole;
        }

        public async Task<Roles> UpdateRoleAsync(int id, Roles updateRole){
            if (id != updateRole.ID)
                return null;

            var role = await _context.Roles.FindAsync(id);
            if (role == null)
                return null;

            role.RoleName = updateRole.RoleName;
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<bool> DeleteRoleAsync(int id){
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
                return false;

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}