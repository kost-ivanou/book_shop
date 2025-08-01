using Application.DTO;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/orders")]

public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders() => Ok(await _orderService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById([FromRoute] int id)
    {
        var order = await _orderService.GetByIdAsync(id);
        if (order == null)
            return NotFound();

        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderDto dto)
    {
        await _orderService.CreateAsync(dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder([FromRoute] int id)
    {
        await _orderService.DeleteAsync(id);
        return Ok();
    }

    [HttpPost("filter")]
    public async Task<IActionResult> FilterOrders([FromBody] OrderFilterDto filter)
    {
        var result = await _orderService.GetFilteredAsync(filter);
        return Ok(result);
    }
}
