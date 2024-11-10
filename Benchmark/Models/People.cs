using Dapper.Helper.DataAttribute;

namespace Benchmark.Models;
[Table("people")]
public class People
{
    [Key]
    public string Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public int Age { get; set; }
}