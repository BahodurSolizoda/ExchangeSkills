using System.Net;
using Dapper;
using Domain.Entities;
using Infrastructure.DataContext;
using Infrastructure.Interfaces;
using Infrastructure.Responses;

namespace Infrastructure.Services;

public class UserService(DapperContext context):IGenericService<User>
{
    public async Task<ApiResponse<List<User>>> GetAll()
    {
        using var connection = context.Connection;
        var sql="select * from users";
        var result=await connection.QueryAsync<User>(sql);
        return new ApiResponse<List<User>>(result.ToList());
    }

    public async Task<ApiResponse<User>> GetById(int id)
    {
        using var connection = context.Connection;
        string sql = "select * from Users where userId = @Id";
        var result = await connection.QuerySingleOrDefaultAsync<User>(sql, new { Id = id });
        if (result == null) return new ApiResponse<User>(HttpStatusCode.NotFound, "User not found");
        return new ApiResponse<User>(result);
    }

    public async Task<ApiResponse<bool>> Add(User data)
    {
        using var connection = context.Connection;
        string sql = @"insert into Users(fullName, Email, Phone, City, createdat)
                      values(@fullName, @Email, @Phone, @City, @createdat)";
        var result = await connection.ExecuteAsync(sql, data);
        if (result == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(true);
    }

    public async Task<ApiResponse<bool>> Update(User data)
    {
        using var connection = context.Connection;
        string sql = @"update Users set fullName = @fullName, email = @email, phone = @phone, 
                      city = @city, createdat = @createdat 
                      where userId = @userId";
        var result = await connection.ExecuteAsync(sql, data);
        if (result == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(true);
    }

    public async Task<ApiResponse<bool>> Delete(int id)
    {
        using var connection = context.Connection;
        string sql = "delete from Users where userId = @Id";
        var res = await connection.ExecuteAsync(sql, new { Id = id });
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.NotFound, "User not found");
        return new ApiResponse<bool>(true);
    }
}