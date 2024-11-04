using Dapper.Helper.DataAttribute;

namespace Dapper.Helper.Test.Models;

public class ModelTestD
{
    [Condition]
    public string foreign_key_a { get; set; }
    [Condition]
    public string foreign_key_b { get; set; }

    public string value { get; set; }
}