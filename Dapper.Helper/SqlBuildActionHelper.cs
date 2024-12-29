using System.Reflection;
using Dapper.Helper.DataAttribute;

namespace Dapper.Helper;
/// <summary>
///     build simple way sql create update and delete entity no have relationship
/// </summary>
public static class SqlBuildActionHelper
{
    public static string Insert<TEntity>() where TEntity : class
    {
        var typeEntity = typeof(TEntity);
        var tableName = SqlHelper.GetTableName(typeEntity);
        var columns = SqlHelper.GetProperties(typeEntity).Select(x => x.Name);
        var sql = $"INSERT INTO {tableName} ({string.Join(", ", columns)}) VALUES ({string.Join(", ",columns.Select(x => String.Concat("@", x)))})";
        return sql;
    }

    public static string Update<TEntity>() where TEntity : class
    {
        var typeEntity = typeof(TEntity);
        var propertyInfos = SqlHelper.GetProperties(typeEntity);
        var tableName = SqlHelper.GetTableName(typeEntity);
        var condition = SqlHelper.GetPropertiesCustomAttribute<Key>(propertyInfos);
        var sql = $"UPDATE {tableName} SET {string.Join(", ", propertyInfos.Select(x=>String.Concat(x.Name," = @", x.Name)))} WHERE {condition}";
        return sql;
    }

    public static string Delete<TEntity>() where TEntity : class
    {
        var typeEntity = typeof(TEntity);
        var tableName = SqlHelper.GetTableName(typeEntity);
        var condition = SqlHelper.GetPropertiesCustomAttribute<Key>(SqlHelper.GetProperties(typeEntity));
        var sql = $"DELETE FROM {tableName} WHERE {condition}";
        return sql;
    }
}