﻿using System;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using OrdersService.Services;

namespace OrdersService.Controllers
{

    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly ServiceOrders _service;
        public OrderController(ServiceOrders service)
        {
            _service = service;
        }
        // GET /api/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAll()
        {
            var orders = await _service.GetAllOrdersAsync();
            return Ok(orders);
        }
        // GET /api/orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetById(int id)
        {
            var order = await _service.GetOrderByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }
        // POST /api/orders
        [HttpPost]
        public async Task<ActionResult<Order>> Create(Order newOrder)
        {
            var created = await _service.CreateOrderAsync(newOrder);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        // PUT /api/orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteOrderAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
