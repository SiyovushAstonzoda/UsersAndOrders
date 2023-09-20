using Dapper;
using Domain.Models.OrderDto;
using Domain.Models.UserDto;
using Infrastructure.Context;

namespace Infrastructure.Services;

public class UserService
{
    private readonly DataContext _context;

    public UserService(DataContext context)
    {
        _context = context;
    }

    //Add User
    public async Task<string> AddUser(AUUserDto user)
    {
        using (var conn = _context.CreateConnection())
        {
            var command = " insert into users (name, email) " +
                          " values (@Name, @Email);";

            var result = await conn.ExecuteAsync(command, new
            {
                Name = user.Name,
                Email = user.Email
            });

            return $"Successfully added {result} user(s)";
        }
    }

    //Update User
    public async Task<string> UpdateUser(AUUserDto user)
    {
        using (var conn = _context.CreateConnection())
        {
            var command = " update users " +
                          " set name = @Name, email = @Email " +
                          " where id = @Id;";

            var result = await conn.ExecuteAsync(command, new
            {
                Name = user.Name,
                Email = user.Email,
                Id = user.Id
            });

            return $"Successfully updated {result} user(s)";
        }
    }

    //Delete User
    public async Task<string> DeleteUser(int id)
    {
        using (var conn = _context.CreateConnection())
        {
            var command = " delete from users " +
                          " where id = @id;";

            var result = await conn.ExecuteAsync(command, new
            {
                Id = id
            });

            return $"Successfully deleted {result} users(s)";
        }
    }

    //Get all Users
    public async Task<IEnumerable<GUserDto>> GetAllUsers()
    {
        using (var conn = _context.CreateConnection())
        {
            var command = " select * " +
                          " from users;";

            var result = await conn.QueryAsync<GUserDto>(command);

            return result;
        }
    }

    //Get User by Id
    public async Task<GUserDto> GetUserById(int id)
    {
        using (var conn = _context.CreateConnection())
        {
            var command = " select * " +
                          " from users " +
                          " where id = @Id;";

            var result = await conn.QuerySingleOrDefaultAsync<GUserDto>(command, new
            {
                Id = id
            });

            return result;
        }
    }

    //Get all Users with list of Orders
    public async Task<IEnumerable<GUsersAndOrdersDto>> GetAllUsersWithOrders()
    {
        using (var conn = _context.CreateConnection())
        {
            /*var command = " select u.name, o.product, o.quantity " +
                          " from users u " +
                          " join orders o " +
                          " on u.id = o.userid " +
                          " group by o.product, o.quantity, u.name " +
                          " order by u.name;";*/

            var command = " select u.name, o.product, sum(o.quantity) as sumquantity " +
                          " from users u " +
                          " join orders o " +
                          " on u.id = o.userid " +
                          " group by u.name, o.product " +
                          " order by u.name;";

            var result = await conn.QueryAsync<GUsersAndOrdersDto>(command);

            return result;
        }
    }

    //Get top Users with a number of Orders
    public async Task<IEnumerable<GTopUsersDto>> GetTopUsers()
    {
        using (var conn = _context.CreateConnection())
        {
            var command = " select u.id, u.name, count(*) as countoforders " +
                          " from orders o " +
                          " join users u " +
                          " on o.userid = u.id " +
                          " group by u.id, u.name " +
                          " order by countoforders desc;";

            var result = await conn.QueryAsync<GTopUsersDto>(command);

            return result;
        }
    }
}
