using System.Data;

namespace Dapper.Helper;

public static class ActionHelper
{
    public static async Task<bool> InsertAsync<TEntity>(this IDbConnection conn, TEntity entity) 
        where TEntity : class
    {
        var sql = SqlBuildQueryHelper.Insert<TEntity>();
        var result = await conn.ExecuteAsync(sql, entity);
        return result > 0;
    }

    public static async Task<bool> UpdateAsync<TEntity>(this IDbConnection conn, TEntity entity)
        where TEntity : class
    {
        var sql = SqlBuildQueryHelper.Update<TEntity>();
        var result = await conn.ExecuteAsync(sql, entity);
        return result > 0;
    }

    public static async Task<bool> DeleteAsync<TEntity>(this IDbConnection conn, TEntity entity)
        where TEntity : class
    {
        var sql = SqlBuildQueryHelper.Delete<TEntity>();
        var result = await conn.ExecuteAsync(sql, entity);
        return result > 0;
    }
}