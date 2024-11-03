namespace Dapper.Helper.Exceptions;

public class TableNoHaveIdentityException()
    : Exception("Table have not identity record i don't know what you want update which record");