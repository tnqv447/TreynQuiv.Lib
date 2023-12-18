using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace TreynQuiv.Lib.Database.EntityFrameworkCore;

/// <summary>
/// Inherits from <see cref="IUnitOfWork"/> and integrates with <see cref="DbContext"/> for <see cref="IDbContextTransaction"/> functionality.
/// </summary>
public interface IEFCoreUnitOfWork : IUnitOfWork, IDisposable
{
    /// <summary>
    /// Start a new transaction.
    /// <para>See Transactions in EF Core for more information and examples.</para>
    /// </summary>
    /// <returns>The started <see cref="IDbContextTransaction"/></returns>
    IDbContextTransaction BeginTransaction();

    /// <summary>
    /// Start a new transaction.
    /// <para>See Transactions in EF Core for more information and examples.</para>
    /// </summary>
    /// <returns>The started <see cref="IDbContextTransaction"/></returns>
    Task<IDbContextTransaction> BeginTransactionAsync();
}
