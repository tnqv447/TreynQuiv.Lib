using System.Linq.Expressions;
using TreynQuiv.Lib.Components;

namespace TreynQuiv.Lib.Database;

/// <summary>
/// A base generic repository interface.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IRepository<TEntity> : ISyncRepository<TEntity>, IAsyncRepository<TEntity> where TEntity : IEntity
{

}
