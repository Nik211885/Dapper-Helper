namespace Dapper.Helper.DataAttribute;

public class Table(string name) : Attribute
{
    public string TableName { get; } = name;
}