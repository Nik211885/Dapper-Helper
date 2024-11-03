using Dapper.Helper.DataAttribute;
using Dapper.Helper;
var sqlInsert = SqlBuildQueryHelper.Insert<People>();
var sqlUpdate = SqlBuildQueryHelper.Update<People>();
var sqlDelete = SqlBuildQueryHelper.Delete<People>();
Console.WriteLine(sqlDelete);

public class People()
{
    [Condition]
    public string name { get; set; }
    [Condition]
    public string age { get; set; }
    public string tets1 { get;  set; }
}
