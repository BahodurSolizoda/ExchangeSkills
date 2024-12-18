using System.Net;
using Dapper;
using Domain.Entities;
using Infrastructure.DataContext;
using Infrastructure.Interfaces;
using Infrastructure.Responses;
using Npgsql;

namespace Infrastructure.Services;

public class RequestService(DapperContext context):IGenericService<Request>
{
    public async Task<ApiResponse<List<Request>>> GetAll()
    {
        using var connection = context.Connection;
        var sql="select * from Skills";
        var result=await connection.QueryAsync<Request>(sql);
        return new ApiResponse<List<Request>>(result.ToList());
    }

    public async Task<ApiResponse<Request>> GetById(int id)
    {
        using var connection = context.Connection;
        string sql = "select * from Requests where requestId = @Id";
        var result = await connection.QuerySingleOrDefaultAsync<Request>(sql, new { Id = id });
        if (result == null) return new ApiResponse<Request>(HttpStatusCode.NotFound, "Request not found");
        return new ApiResponse<Request>(result);
    }

    public async Task<ApiResponse<bool>> Add(Request data)
    {
        using NpgsqlConnection connection = context.Connection;

        string sql = @"insert into Requests(FromUserId, ToUserId, RequestedSkillId, OfferedSkillId, Status, CreatedAt, UpdatedAt)
                   values(@FromUserId, @ToUserId, @RequestedSkillId, @OfferedSkillId, CAST(@Status AS request_status), @CreatedAt, @UpdatedAt)";

        var result = await connection.ExecuteAsync(sql, new
        {
            data.FromUserId,
            data.ToUserId,
            data.RequestedSkillId,
            data.OfferedSkillId,
            Status = data.Status.ToString().ToLower(), 
            data.CreatedAt,
            data.UpdatedAt
        });

        if (result == 0)
            return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");

        return new ApiResponse<bool>(true);
    }


    public async Task<ApiResponse<bool>> Update(Request data)
    {
        using var connection = context.Connection;
        string sql = @"update Requests set FromUserId=@FromUserId, ToUserId=@ToUserId, RequestedSkillId=@RequestedSkillId, OfferedSkillId=@OfferedSkillId, Status=@Status, CreatedAt=@CreatedAt, UpdatedAt=@UpdatedAt
                      where requestId = @requestId";
        var result = await connection.ExecuteAsync(sql, data);
        if (result == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(true);
    }

    public async Task<ApiResponse<bool>> Delete(int id)
    {
        using var connection = context.Connection;
        string sql = "delete from Requests where requestId = @Id";
        var res = await connection.ExecuteAsync(sql, new { Id = id });
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.NotFound, "Request not found");
        return new ApiResponse<bool>(true);
    }
}