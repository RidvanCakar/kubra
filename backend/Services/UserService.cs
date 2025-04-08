using System.Runtime.CompilerServices;
using backend.Models;
using Context;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;


namespace Services
{
    public class UserService
    {
        private readonly eCommerceContext _context;
        private readonly IConfiguration _configuration;


        public UserService(eCommerceContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }

        public async Task<List<User>> GetUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users?.Any() == true ? users : null;

        }

        public async Task<User> GetUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user ?? null;
        }

        public async Task<User> RegisterUserAsync(User user, int roleID)
        {
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
            {
                return null;
            }
            var newUser = new User
            {
                Username = user.Username,
                Email = user.Email,
                Password = HashPassword(user.Password),
            };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            var userRoles = new UserRoles
            {
                UserID = newUser.ID,
                RoleID = roleID
            };

            _context.UserRoles.Add(userRoles);
            await _context.SaveChangesAsync();

            return newUser;
        }


        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        private bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            string hashedInput = HashPassword(inputPassword);
            return hashedInput == hashedPassword;
        }


        public async Task<string> LoginUserAsync(User loginUser)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginUser.Email);
            if (user == null)
            {
                Console.WriteLine($"Kullanıcı bulunamadı:{loginUser.Email}");
                return "Geçersiz kullanıcı adı";
            }
            if (!VerifyPassword(loginUser.Password, user.Password))
            {
                Console.WriteLine($"Şifre eşleşmedi:{loginUser.Email}");
                return "Geçersiz şifre";
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.ID.ToString())
        };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;

        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> UpdateUserAsync(int id, User updateUser)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return null;
            }

            user.Username = updateUser.Username;
            user.Email = updateUser.Email;
            user.Password = updateUser.Password;

            await _context.SaveChangesAsync();
            return user;
        }

    }
}