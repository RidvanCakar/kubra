using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserRolesController : ControllerBase
    {

        private readonly UserRolesService _userRolesService;

        public UserRolesController(UserRolesService userRolesService)
        {
            _userRolesService = userRolesService;
        }



        [HttpGet]
        public async Task<IActionResult> GetUserRoles()
        {
            var userRoles = await _userRolesService.GetUserRolesAsync();
            if (userRoles.Count > 0)
            {
                return Ok(userRoles);
            }
            else
            {
                return NotFound("Kullanıcı Bulunamadı");
            }

        }


        [HttpGet("{UserID}")]
        public async Task<IActionResult> GetUserRole(int UserID)
        {
            var userRole = await _userRolesService.GetUserRoleAsync(UserID);
            if (userRole != null)
            {
                return Ok(userRole);
            }
            else
            {
                return NotFound("Kullanıcı Bulunamadı");
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddUserRole([FromBody] UserRoles userRoles)
        {
            var lastUserRole = await _userRolesService.AddUserRoleAsync(userRoles);
            if (lastUserRole != null)
            {
                return Ok(new { message = "Rol Eklendi", userRoles });
            }
            else
            {
                return BadRequest("Rol eklenemedi");
            }
        }



        [HttpPut("{UserID}")]

        public async Task<IActionResult> PutUserRoles(int UserID, UserRoles updateUserRoles)
        {
            if (UserID != updateUserRoles.UserID)
            {
                return BadRequest("Kullanıcı ID'si uyuşmuyor");
            }

            var userRole = await _userRolesService.UpdateUserRoleAsync(UserID, updateUserRoles);
            if (userRole == null)
            {
                return NotFound("Kullanıcı Bulunamadı");
            }

            return Ok(new { message = "Rol güncellendi", userRole });

        }



    }
}