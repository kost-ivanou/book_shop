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
    public async Task<ActionResult<List<OrderResponseDto>>> GetOrders()
    {
        var orders = await _orderService.GetAllAsync();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderResponseDto>> GetOrderById([FromRoute] int id)
    {
        var order = await _orderService.GetByIdAsync(id);
        if (order == null)
            return NotFound();

        return Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateOrder([FromBody] OrderDto dto)
    {
        int id = await _orderService.CreateAsync(dto);
        return Ok(id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder([FromRoute] int id)
    {
        if(!await _orderService.DeleteAsync(id))
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpGet("filter")]
    public async Task<ActionResult<List<OrderResponseDto>>> FilterOrders([FromQuery] OrderFilterDto filter)
    {
        var result = await _orderService.GetFilteredAsync(filter);
        return Ok(result);
    }
}
