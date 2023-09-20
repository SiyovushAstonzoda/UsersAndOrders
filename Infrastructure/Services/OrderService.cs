using Dapper;
using Domain.Models.OrderDto;
using Infrastructure.Context;

namespace Infrastructure.Services;

public class OrderService
{
    private readonly DataContext _context;

    public OrderService(DataContext context)
    {
        _context = context;
    }

    //Add Order
    public async Task<string> AddOrder(AUOrderDto order)
    {
        using (var conn = _context.CreateConnection()) 
        {
            var command = " insert into orders (product, quantity, userid) " +
                          " values (@Product, @Quantity, @UserId);";

            var result = await conn.ExecuteAsync(command, new
            {
                Product = order.Product,
                Quantity = order.Quantity,
                UserId = order.UserId
            });

            return $"Successfully added {result} order(s)";
        }
    }

    //Update Order
    public async Task<string> UpdateOrder(AUOrderDto order)
    {
        using (var conn = _context.CreateConnection()) 
        {
            var command = " update orders " +
                          " set product = @Product, quantity = @Quantity, userid = @UserId " +
                          " where id = @Id;";

            var result = await conn.ExecuteAsync(command, new
            {
                Product = order.Product,
                Quantity = order.Quantity,
                UserId = order.UserId,
                Id = order.Id
            });

            return $"Successfully updated {result} order(s)";
        }
    }

    //Delete Order
    public async Task<string> DeleteOrder(int id)
    {
        using (var conn = _context.CreateConnection())
        {
            var command = " delete from orders " +
                          " where id = @Id;";

            var result = await conn.ExecuteAsync(command, new
            {
                Id = id
            });

            return $"Successfully deleted {result} order(s)";
        }
    }

    //Get all Orders
    public async Task<IEnumerable<GOrderDto>> GetAllOrders()
    {
        using (var conn = _context.CreateConnection())
        {
            var command = " select * " +
                          " from orders;";

            var result = await conn.QueryAsync<GOrderDto>(command);

            return result;
        }
    }

    //Get Order by Id
    public async Task<GOrderDto> GetOrderById(int id)
    {
        using (var conn = _context.CreateConnection()) 
        {
            var command = " select * " +
                          " from orders " +
                          " where id = @Id;";

            var result = await conn.QuerySingleOrDefaultAsync<GOrderDto>(command, new
            {
                Id = id 
            });

            return result;
        }
    }

    //Get Order filtering by Product
    public async Task<IEnumerable<GetOrderByProductDto>> GetOrderByProduct(string product)
    {
        using (var conn = _context.CreateConnection())
        {
            var command = " select o.product, o.userid, u.name as username, sum(o.quantity) as sumquantity " +
                          " from orders o " +
                          " join users u " +
                          " on o.userid = u.id " +
                          $" where lower(o.product) like '{product.ToLower()}%' " +
                          " group by o.product, o.userid, u.name;";

            var result = await conn.QueryAsync<GetOrderByProductDto>(command);

            return result;
        }
    }

}
