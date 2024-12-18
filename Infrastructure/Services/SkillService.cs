using System.Net;
using Dapper;
using Domain.Entities;
using Infrastructure.DataContext;
using Infrastructure.Interfaces;
using Infrastructure.Responses;

namespace Infrastructure.Services;

public class SkillService(DapperContext context):IGenericService<Skill>
{
    public async Task<ApiResponse<List<Skill>>> GetAll()
    {
        using var connection = context.Connection;
        var sql="select * from Skills";
        var result=await connection.QueryAsync<Skill>(sql);
        return new ApiResponse<List<Skill>>(result.ToList());
    }

    public async Task<ApiResponse<Skill>> GetById(int id)
    {
        using var connection = context.Connection;
        string sql = "select * from Skills where skillId = @Id";
        var result = await connection.QuerySingleOrDefaultAsync<Skill>(sql, new { Id = id });
        if (result == null) return new ApiResponse<Skill>(HttpStatusCode.NotFound, "User not found");
        return new ApiResponse<Skill>(result);
    }

    public async Task<ApiResponse<bool>> Add(Skill data)
    {
        using var connection = context.Connection;
        string sql = @"insert into Skills(UserId, Tittle, Description, CreatedAt)
                      values(@UserId, @Tittle, @Description, @CreatedAt)";
        var result = await connection.ExecuteAsync(sql, data);
        if (result == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(true);
    }

    public async Task<ApiResponse<bool>> Update(Skill data)
    {
        using var connection = context.Connection;
        string sql = @"update Skills set userId = @userId, tittle = @tittle, description = @description, createdAt = @createdAt
                      where skillId = @skillId";
        var result = await connection.ExecuteAsync(sql, data);
        if (result == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(true);
    }

    public async Task<ApiResponse<bool>> Delete(int id)
    {
        using var connection = context.Connection;
        string sql = "delete from Skills where skillId = @Id";
        var res = await connection.ExecuteAsync(sql, new { Id = id });
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.NotFound, "Skill not found");
        return new ApiResponse<bool>(true);
    }
}