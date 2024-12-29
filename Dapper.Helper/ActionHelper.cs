using System.Data;

namespace Dapper.Helper;

public static class ActionHelper
{
    public static async Task<bool> InsertAsync<TEntity>(this IDbConnection conn, TEntity entity) 
        where TEntity : class
    {
        var sql = SqlBuildActionHelper.Insert<TEntity>();
        var result = await conn.ExecuteAsync(sql, entity);
        return result > 0;
    }

    public static async Task<bool> UpdateAsync<TEntity>(this IDbConnection conn, TEntity entity)
        where TEntity : class
    {
        var sql = SqlBuildActionHelper.Update<TEntity>();
        var result = await conn.ExecuteAsync(sql, entity);
        return result > 0;
    }
    
    public static async Task<bool> DeleteAsync<TEntity>(this IDbConnection conn, TEntity entity)
        where TEntity : class
    {
        var sql = SqlBuildActionHelper.Delete<TEntity>();
        var result = await conn.ExecuteAsync(sql, entity);
        return result > 0;
    }
}