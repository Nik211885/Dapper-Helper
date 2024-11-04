using Dapper.Helper.DataAttribute;

namespace Dapper.Helper.Test.Models;
[Table("model_a")]
public class ModelTestA
{
    [Condition]
    public string id { get; set; }
    public string name { get; set; }
    public string age { get; set; }
}