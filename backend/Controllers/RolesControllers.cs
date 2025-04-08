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
    public class RolesControllers : ControllerBase
    {
        private readonly RolesService _rolesService;

        public RolesControllers(RolesService rolesService)
        {
            _rolesService= rolesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles=await _rolesService.GetRolesAsync();
            if(roles.Count>0){
                return Ok(roles);
            }else{
                return NotFound("Rol Bulunamadı");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole(int id)
        {
            var role=await _rolesService.GetRoleAsync(id);
            if(role!=null){
                return Ok(role);
            }else{
                return NotFound("Rol Bulunamadı");
            }   
        }

        [HttpPost]
        public async Task<IActionResult> PostRole([FromBody] Roles role)
        {
           var roles=await _rolesService.AddRoleAsync(role);
            if(roles==null){
                return BadRequest("Rol Eklenemedi");
            }
            return Ok(new { message = "Rol Eklendi", roles });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(int id,[FromBody] Roles roles){
            var role=await _rolesService.UpdateRoleAsync(id,roles);
            if(role == null){
                return BadRequest("Rol Güncellenemedi");
            }
            return Ok(new { message = "Rol Güncellendi", roles });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role=await _rolesService.DeleteRoleAsync(id);
            if(role==false){
                return BadRequest("Rol Silinemedi");
            }
            return Ok(new { message = "Rol Silindi" });
        }
    }
}