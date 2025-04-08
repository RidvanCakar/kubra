using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;

namespace backend.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]

    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderService.GetOrdersAsync();
            if (orders.Count > 0)
            {
                return Ok(orders);
            }
            else
            {
                return NotFound("Sipariş Bulunamadı");
            }

        }

        [HttpGet("{UserId}")]

        public async Task<IActionResult> GetOrder(int UserId)
        {
            var order = await _orderService.GetOrderAsync(UserId);
            if (order.Count > 0)
            {
                return Ok(order);
            }
            else
            {
                return NotFound("Sipariş Bulunamadı");
            }
        }



        [HttpPost]
        public async Task<IActionResult> SaveOrder([FromBody] List<Order> orders)
        {
            var order = await _orderService.SaveOrderAsync(orders);
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


