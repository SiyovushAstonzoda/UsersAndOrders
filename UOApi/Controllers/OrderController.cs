using Domain.Models.OrderDto;
using Domain.Models.UserDto;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace UOApi.Controllers;

[ApiController]
[Route("[controller]")]

public class OrderController
{
    private readonly OrderService _orderService;

    public OrderController(OrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost("Create Order")]
    public async Task<string> CreateOrder(AUOrderDto order)
    {
        return await _orderService.AddOrder(order);
    }

    [HttpPut("Update Order")]
    public async Task<string> UpdateOrder(AUOrderDto order)
    {
        return await _orderService.UpdateOrder(order);
    }

    [HttpDelete("Delete Order")]
    public async Task<string> DeleteOrder(int id)
    {
        return await _orderService.DeleteOrder(id);
    }

    [HttpGet("Get All Orders")]
    public async Task<IEnumerable<GOrderDto>> GetAllOrders()
    {
        return await _orderService.GetAllOrders();
    }

    [HttpGet("Get Order ById")]
    public async Task<GOrderDto> GetOrderById(int id)
    {
        return await _orderService.GetOrderById(id);
    }

    [HttpGet("Get Order filtering by Product")]
    public async Task<IEnumerable<GetOrderByProductDto>> GetOrderByProduct(string product)
    {
        return await _orderService.GetOrderByProduct(product);
    }
}

