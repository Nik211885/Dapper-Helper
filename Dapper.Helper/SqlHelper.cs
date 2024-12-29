using System.Reflection;
using Dapper.Helper.DataAttribute;
using Dapper.Helper.Exceptions;

namespace Dapper.Helper;

public static class SqlHelper
{
    public static string GetTableName(Type type)
    {
        var tableClass = type.GetCustomAttributes<Table>().FirstOrDefault();
        return tableClass is null ? type.Name : tableClass.TableName;
    }

    public static PropertyInfo[] GetProperties(Type type) 
        => type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty);

    public static string GetPropertiesCustomAttribute<TAttribute>(PropertyInfo [] propertyInfos)
        where TAttribute : Attribute
    {
        var propertyCondition = propertyInfos.Where(x => x.GetCustomAttribute<TAttribute>() is not null).ToList();
        if (!propertyCondition.Any())
        {
            propertyCondition.Add(propertyInfos.FirstOrDefault(x => x.Name.ToUpper().Equals("ID")) 
                                  ?? throw new TableNoHaveIdentityException());
        }
        var condition = string.Join(" AND ", propertyCondition.Select(x => string.Concat(x.Name, " = @", x.Name)));
        return condition;
    }
}