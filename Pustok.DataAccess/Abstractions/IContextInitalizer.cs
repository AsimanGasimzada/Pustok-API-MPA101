namespace Pustok.DataAccess.Abstractions;

public interface IContextInitalizer
{
    Task InitDatabaseAsync();
}
