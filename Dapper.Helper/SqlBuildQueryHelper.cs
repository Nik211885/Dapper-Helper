using System.Reflection;
using Dapper.Helper.DataAttribute;
using Dapper.Helper.Exceptions;

namespace Dapper.Helper;

public static class SqlBuildQueryHelper
{
    public static string Insert<TEntity>() where TEntity : class
    {
        var typeEntity = typeof(TEntity);
        var tableName = GetTableName(typeEntity);
        var columns = GetProperties(typeEntity).Select(x => x.Name);
        var sql = $"INSERT INTO {tableName} ({string.Join(", ", columns)}) VALUES ({string.Join(", ",columns.Select(x => String.Concat("@", x)))})";
        return sql;
    }

    public static string Update<TEntity>() where TEntity : class
    {
        var typeEntity = typeof(TEntity);
        var propertyInfos = GetProperties(typeEntity);
        var tableName = GetTableName(typeEntity);
        var condition = GetCondition(propertyInfos);
        var sql = $"UPDATE {tableName} SET {string.Join(", ", propertyInfos.Select(x=>String.Concat(x.Name," = @", x.Name)))} WHERE {condition}";
        return sql;
    }

    public static string Delete<TEntity>() where TEntity : class
    {
        var typeEntity = typeof(TEntity);
        var tableName = GetTableName(typeEntity);
        var condition = GetCondition(GetProperties(typeEntity));
        var sql = $"DELETE FROM {tableName} WHERE {condition}";
        return sql;
    }

    private static string GetTableName(Type type)
    {
        var tableClass = type.GetCustomAttributes<Table>().FirstOrDefault();
        return tableClass is null ? type.Name : tableClass.TableName;
    }

    private static PropertyInfo[] GetProperties(Type type) 
        => type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty);

    private static string GetCondition(PropertyInfo [] propertyInfos)
    {
        var propertyCondition = propertyInfos.Where(x => x.GetCustomAttribute<Key>() is not null).ToList();
        if (!propertyCondition.Any())
        {
            propertyCondition.Add(propertyInfos.FirstOrDefault(x => x.Name.ToUpper().Equals("ID")) ?? throw new TableNoHaveIdentityException());
        }
        var condition = string.Join(" AND ", propertyCondition.Select(x => string.Concat(x.Name, " = @", x.Name)));
        return condition;
    }
}