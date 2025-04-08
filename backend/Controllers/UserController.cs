using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var user = await _userService.GetUsersAsync();
            if (user.Count > 0)
            {
                return Ok(user);
            }
            else
            {
                return NotFound("Kullanıcı Bulunamadı");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUserAsync(id);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound("Kullanıcı Bulunamadı");
            }
        }


        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] User user, int roleID)
        {
            var newUser = await _userService.RegisterUserAsync(user, roleID);
            if (newUser == null)
            {
                return BadRequest("Kullanıcı zaten mevcut");
            }

            return Ok(new { message = "Kayıt başarılı", user = newUser });
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] User loginUser)
        {
            var user = await _userService.LoginUserAsync(loginUser);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok();
            }

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

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userService.DeleteUserAsync(id);
            if (user == null)
            {
                return NotFound("Kullanıcı Bulunamadı");
            }
            return Ok(new { message = "Kullanıcı silindi", user });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User updateUser)
        {
            var user = await _userService.UpdateUserAsync(id, updateUser);
            if (user == null)
            {
                return NotFound("Kullanıcı Bulunamadı");
            }
            return Ok(new { message = "Kullanıcı güncellendi", user });
        }
    }
}