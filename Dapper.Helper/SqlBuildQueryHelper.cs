using System.Reflection;
using Dapper.Helper.DataAttribute;
using Dapper.Helper.Exceptions;

namespace Dapper.Helper;

public static class SqlBuildQueryHelper
{
    public static string Insert<TEntity>() where TEntity : class
    {
        var typeEntity = typeof(TEntity);
        var tableClass = typeEntity.GetCustomAttributes<Table>().FirstOrDefault();
        var tableName = tableClass is null? typeEntity.FullName : tableClass.TableName;
        var columns = typeEntity.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty).Select(x => x.Name);
        var sql = $"INSERT INTO {tableName} ({string.Join(", ", columns)}) VALUES ({string.Join(", ",columns.Select(x => String.Concat("@", x)))})";
        return sql;
    }

    public static string Update<TEntity>() where TEntity : class
    {
        var typeEntity = typeof(TEntity);
        var propertyInfos =
            typeEntity.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);  
        var tableClass = typeEntity.GetCustomAttributes<Table>().FirstOrDefault();
        var propertyCondition = propertyInfos.Where(x => x.GetCustomAttribute<Condition>() is not null).ToList();
        if (!propertyCondition.Any())
        {
            propertyCondition.Add(propertyInfos.FirstOrDefault(x => x.Name.ToUpper().Equals("ID")) ?? throw new TableNoHaveIdentityException());
        }
        var tableName = tableClass is null? typeEntity.Name : tableClass.TableName;
        var condition = string.Join(" AND ", propertyCondition.Select(x => string.Concat(x.Name, " = @", x.Name)));
        var sql = $"UPDATE {tableName} SET {string.Join(", ", propertyInfos.Select(x=>String.Concat(x.Name," = @", x.Name)))} WHERE {condition}";
        return sql;
    }

    public static string Delete<TEntity>() where TEntity : class
    {
        var typeEntity = typeof(TEntity);
        var propertyInfos =
            typeEntity.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);  
        var tableClass = typeEntity.GetCustomAttributes<Table>().FirstOrDefault();
        var propertyCondition = propertyInfos.Where(x => x.GetCustomAttribute<Condition>() is not null).ToList();
        if (!propertyCondition.Any())
        {
            propertyCondition.Add(propertyInfos.FirstOrDefault(x => x.Name.ToUpper().Equals("ID")) ?? throw new TableNoHaveIdentityException());
        }
        var tableName = tableClass is null? typeEntity.Name : tableClass.TableName;
        var condition = string.Join(" AND ", propertyCondition.Select(x => string.Concat(x.Name, " = @", x.Name)));
        var sql = $"DELETE FROM {tableName} WHERE {condition}";
        return sql;
    }
}