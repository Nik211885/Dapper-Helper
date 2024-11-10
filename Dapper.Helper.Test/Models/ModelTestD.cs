using Dapper.Helper.DataAttribute;

namespace Dapper.Helper.Test.Models;

public class ModelTestD
{
    [Key]
    public string foreign_key_a { get; set; }
    [Key]
    public string foreign_key_b { get; set; }

    public string value { get; set; }
}