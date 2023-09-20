using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Infrastructure.Context;

public class DataContext
{
    private readonly IConfiguration _configuration;

    public DataContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection CreateConnection()
    {
        var connection = _configuration.GetConnectionString("SqlConnection");
        return new NpgsqlConnection(connection);
    }
}
