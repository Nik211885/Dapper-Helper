using System.Data;
using Benchmark.Models;
using Dapper.Helper;
using Npgsql;

namespace Benchmark;

public class DapperHelperTest
{
    private readonly IDbConnection _connection;

    public DapperHelperTest(string connectionString)
    {
        _connection = new NpgsqlConnection(connectionString);
        _connection.Open();
    }

    public async Task<bool> InsertPeopleAsync(People people)
    {
        //validation here
        var result = await _connection.InsertAsync(people);
        return result;
    }

    public async Task<bool> UpdatePeopleAsync(People people)
    {
        var result = await _connection.UpdateAsync(people);
        return result;
    }

    public async Task<bool> DeletePeopleAsync(People people)
    {
        var result = await _connection.DeleteAsync(people);
        return result;
    }
}