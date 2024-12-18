using Npgsql;

namespace Infrastructure.DataContext;

public class DapperContext
{
    private readonly string _connectionString =
        "Host = localhost;port=5432;database = ExchangeSkillsdb;username = postgres;password = 7070";

    public NpgsqlConnection Connection
    {
        get
        {
            return new NpgsqlConnection(_connectionString);

        }
    }
}