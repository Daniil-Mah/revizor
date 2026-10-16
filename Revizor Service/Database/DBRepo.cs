using Dapper;
using Npgsql;
using Revizor_Service.Database.Models;

namespace Revizor_Service.Database;

public class DBRepo
{
    private readonly string _connectionString;

    public DBRepo(IConfiguration cfg)
    {   
        _connectionString = cfg.GetConnectionString("revizor")!;
    }

    private async Task<IEnumerable<T>> QueryAsync<T>(string sql,object? param = null)
    {
        using var con = new NpgsqlConnection(_connectionString);
        return await con.QueryAsync<T>(sql,param);
    }

    private async Task<T?> QueryFirstAsync<T>(string sql, object? param = null)
    {
        using var con = new NpgsqlConnection(_connectionString);
        return await con.QueryFirstOrDefaultAsync<T>(sql,param);
    }

    private async Task<int> ExecuteAsync(string sql, object? param = null)
    {
        using var con = new NpgsqlConnection(_connectionString);
        return await con.ExecuteAsync(sql, param);
    }

    public Task<IEnumerable<T>> GetRepotrs<T>() => QueryAsync<T>("select * from records order by userid");

    public Task<User?> GetUserByRefresh(string refreshToken) => QueryFirstAsync<User>("select * from users where refreshtoken=@Token",new {Token=refreshToken});

    public Task<Plan?> GetCurrentPlan() =>  QueryFirstAsync<Plan>("select * from plan where isarchived = false");
    public Task<int> ArchivedPlan() => ExecuteAsync("update plan set 'isarchived' = true");

    public Task<int> PatchEntity(string tableName,object dto, Guid id)
    {
        var update = dto.GetType().GetProperties().Where(p => p.GetValue(dto) is not null).ToDictionary(p=> p.Name, p=> p.GetValue(dto));
        if (update.Count == 0)
        {
            return Task.FromResult(0);
        }
        var values = string.Join(", ", update.Keys.Select(k=> $"{k} = @{k}"));
        var sql = $"update {tableName} set {values} where id = @Id";
        var param = new DynamicParameters(update);
        param.Add("Id",id);
        return ExecuteAsync(sql,param);
    }

    public Task<int> PatchEntity(string tableName,object dto, int id)
    {
        var update = dto.GetType().GetProperties().Where(p => p.GetValue(dto) is not null).ToDictionary(p=> p.Name, p=> p.GetValue(dto));
        if (update.Count == 0)
        {
            return Task.FromResult(0);
        }
        var values = string.Join(", ", update.Keys.Select(k=> $"{k} = @{k}"));
        var sql = $"update {tableName} set {values} where id = @Id";
        var param = new DynamicParameters(update);
        param.Add("Id",id);
        return ExecuteAsync(sql,param);
    }

    // var successCreated = 0 ;
    // Потом добавить счетчик удачных созданных зааписей. потому что сейчас чуть лень думать над тем как правильно возвращать значение у этого метода
    public async Task CreateEntities (string tableName, List<PlanAssigment> dtos)
    {

        foreach (var item in dtos)
        {
            await CreateEntity("planassigment",item);
        }
        return ;
    }

    public Task<int> CreateEntity (string tableName, object dto)
    {
        var fields = dto.GetType().GetProperties().Where(p => p.GetValue(dto) is not null).ToDictionary(p=> p.Name, p=> p.GetValue(dto));
        var columns = string.Join(", ", fields.Keys);
        var values = string.Join(", ",fields.Keys.Select(k=> $"@{k}"));
        var sql = $"insert into {tableName} ({columns}) values ({values})";
        var param = new DynamicParameters(fields);
        return ExecuteAsync(sql, param);
    }

    public Task<T?> GetEntityByName<T>(string table, string name) => QueryFirstAsync<T>(@$"select * from {table} where Name = @Name", new { Name = name });
    public Task<IEnumerable<T>> GetEntities<T>(string table, int limit = 50, int offset = 0) => QueryAsync<T>($"select * from {table} limit @limit offset @offset", new { limit = limit, offset = offset });
    public Task<int> DeleteEntity (string table, Guid id) => ExecuteAsync($"delete from {table} where id = @Id", new {Id = id});

    public Task<int> DeleteEntity (string table, int id) => ExecuteAsync($"delete from {table} where id = @Id", new {Id = id});

    public Task<T?> GetEntity<T>(string table, Guid id) => QueryFirstAsync<T>($"select * from {table} where Id = @Id", new {Id = id}); 
    public Task<T?> GetEntity<T>(string table, int id) => QueryFirstAsync<T>($"select * from {table} where Id = @Id", new {Id = id}); 
}