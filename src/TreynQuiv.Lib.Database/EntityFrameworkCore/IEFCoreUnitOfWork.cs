using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace TreynQuiv.Lib.Database.EntityFrameworkCore;

/// <summary>
/// Inherits from <see cref="IUnitOfWork"/> and integrates with <see cref="DbContext"/> for <see cref="IDbContextTransaction"/> functionality.
/// </summary>
public interface IEFCoreUnitOfWork : IUnitOfWork, IDisposable
{
    IDbContextTransaction BeginTransaction();
    Task<IDbContextTransaction> BeginTransactionAsync();
}
