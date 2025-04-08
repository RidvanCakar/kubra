using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
    public class ShoppingCardController : ControllerBase
    {
        private readonly ShoppingCardService _shoppingCardService;

        public ShoppingCardController(ShoppingCardService shoppingCardService)
        {
            _shoppingCardService = shoppingCardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetShoppingCards()
        {
            var shoppingCards = await _shoppingCardService.GetShoppingCardsAsync();
            if (shoppingCards.Count > 0)
            {
                return Ok(shoppingCards);
            }
            else
            {
                return NotFound("Sepet Bulunamadı");
            }
        }

        [HttpGet("{UserId}")]

        public async Task<IActionResult> GetShoppingCard(int UserId)
        {
            
            var shoppingCard = await _shoppingCardService.GetShoppingCardAsync(UserId);
            if (shoppingCard.Count > 0)
            {
                return Ok(shoppingCard);
            }
            else
            {
                return NotFound("Sepet Bulunamadı");
            }
        }

        [HttpPost]

        public async Task<IActionResult> PostShoppingCard([FromBody] ShoppingCard shoppingCard)
        {
            var product = await _shoppingCardService.AddShoppingCardAsync(shoppingCard);
            if (product == null)
            {
                return BadRequest("Sepet Eklenemedi");
            }
            return Ok(new { message = "Sepet Eklendi", product });
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteShoppingCard(int Id)
        {
            var DeleteProduct=await _shoppingCardService.DeleteShoppingCardAsync(Id);
            if (DeleteProduct == false) 
            {
                return NotFound("Ürün Bulunamadı");
            }
            else
            {
                return Ok(new { message = "Ürün Silindi" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostOrder([FromBody] Order orders)
        {
            var order = await _shoppingCardService.AddOrderAsync(orders);
            if (order != null)
            {
                return Ok(new { message = "Sipariş Alındı", order });
            }
            else
            {
                return BadRequest("Sipariş Alınamadı");
            }
        }


    }
}

