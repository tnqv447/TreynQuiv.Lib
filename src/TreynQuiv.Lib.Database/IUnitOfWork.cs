namespace TreynQuiv.Lib.Database;

/// <summary>
/// A base Unit of Work interface.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    void SaveChanges();
    Task SaveChangesAsync();
}
