using Dapper.Helper.Exceptions;
using Dapper.Helper.Test.Models;

namespace Dapper.Helper.Test;

public class SqlBuilderQueryHelperTest
{
    [Fact]
    public void SqlBuilderQueryHelper_Insert_ReturnSqlString()
    {
        var result = "INSERT INTO model_a (id, name, age) VALUES (@id, @name, @age)";
        var sql = SqlBuildActionHelper.Insert<ModelTestA>();
        Assert.Equal(result, sql);
    }

    [Fact]
    public void SqlBuilderQueryHelper_Update_ThrowTableNoHaveIdentityException()
    {
        Action getSql = () => SqlBuildActionHelper.Update<ModelTestC>();
        Assert.Throws<TableNoHaveIdentityException>(getSql);
    }
    
    [Fact]
    public void SqlBuilderQueryHelper_Update_ReturnSqlString()
    {
        var result = "UPDATE ModelTestB SET id = @id, name = @name, age = @age WHERE id = @id";
        var sql = SqlBuildActionHelper.Update<ModelTestB>();
        Assert.Equal(result, sql);
    }
    [Fact]
    public void SqlBuilderQueryHelper_Update_ReturnSqlWithTwoConditionString()
    {
        var result = "UPDATE ModelTestD SET foreign_key_a = @foreign_key_a, foreign_key_b = @foreign_key_b, value = @value WHERE foreign_key_a = @foreign_key_a AND foreign_key_b = @foreign_key_b";
        var sql = SqlBuildActionHelper.Update<ModelTestD>();
        Assert.Equal(result, sql);
    }
    [Fact]
    public void SqlBuilderQueryHelper_Delete_ThrowTableNoHaveIdentityException()
    {
        Action getSql = () => SqlBuildActionHelper.Delete<ModelTestC>();
        Assert.Throws<TableNoHaveIdentityException>(getSql);
    }
    [Fact]
    public void SqlBuilderQueryHelper_Delete_ReturnSqlString()
    {
        var result = "DELETE FROM ModelTestB WHERE id = @id";
        var sql = SqlBuildActionHelper.Delete<ModelTestB>();
        Assert.Equal(result, sql);
    }
    [Fact]
    public void SqlBuilderQueryHelper_Delete_ReturnSqlWithTwoConditionString()
    {
        var result = "DELETE FROM ModelTestD WHERE foreign_key_a = @foreign_key_a AND foreign_key_b = @foreign_key_b";
        var sql = SqlBuildActionHelper.Delete<ModelTestD>();
        Assert.Equal(result, sql);
    }

}
