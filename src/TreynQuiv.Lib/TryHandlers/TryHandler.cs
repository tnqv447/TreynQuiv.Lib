using System.Runtime.CompilerServices;

namespace TreynQuiv.Lib.TryHandlers;

/// <summary>
/// Reusable and short hand for try catch block to execute actions.
/// </summary>
public class TryHandler
{
    /// <summary>
    /// An <see cref="Action"/> before try-catch execution.
    /// </summary>
    /// <value>Default <see cref="null"/>.</value>
    public Action? BeforeAction { get; init; }

    /// <summary>
    /// An <see cref="Action"/> after try-catch execution.
    /// </summary>
    /// <value>Default <see cref="null"/>.</value>
    public Action? AfterAction { get; init; }

    /// <summary>
    /// An <see cref="Action"/> that handles <see cref="Exception"/> in catch block.
    /// </summary>
    public required Action<Exception> ExceptionHandleAction { get; init; }

    /// <summary>
    /// An <see cref="Action"/> in finally block.
    /// </summary>
    /// <value>Default <see cref="null"/>.</value>
    public Action? FinallyAction { get; init; }

    /// <summary>
    /// Execute action in try-catch block with lo
    /// </summary>
    /// <param name="action"></param>
    /// <param name="localExceptionHandleAction"></param>
    /// <returns></returns>
    public virtual bool Try(Action action, Action<Exception>? localExceptionHandleAction = null)
    {
        try
        {
            BeforeAction?.Invoke();
            action.Invoke();
            AfterAction?.Invoke();
            return true;
        }
        catch (Exception ex)
        {
            ExceptionHandleAction.Invoke(ex);
            localExceptionHandleAction?.Invoke(ex);
        }
        finally
        {
            FinallyAction?.Invoke();
        }

        return false;
    }

    public virtual TryResult<T> Try<T>(Func<T?> action, Action<Exception>? localExceptionHandleAction = null)
    {
        try
        {
            BeforeAction?.Invoke();
            var result = action.Invoke();
            AfterAction?.Invoke();
            return new(true, result);
        }
        catch (Exception ex)
        {
            ExceptionHandleAction.Invoke(ex);
            localExceptionHandleAction?.Invoke(ex);
        }
        finally
        {
            FinallyAction?.Invoke();
        }

        return new(false, default);
    }

    public virtual TryEnsuredResult<T> TryEnsure<T>(Func<T> action, Action<Exception>? localExceptionHandleAction = null) where T : new()
    {
        var tryResult = Try(action, localExceptionHandleAction);
        return tryResult ? new(true, tryResult.Value ?? new()) : new(false, new());
    }

    public virtual TryEnsuredResult<T> TryFallBack<T>(in T fallbackValue,
                                       Func<T> action,
                                       Action<Exception>? localExceptionHandleAction = null)
    {
        var tryResult = Try(action, localExceptionHandleAction);
        return tryResult ? new(true, tryResult.Value ?? fallbackValue) : new(false, fallbackValue);
    }

    #region Inline

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual void TryInline(Action action, Action<Exception>? localExceptionHandleAction = null)
    {
        _ = Try(action, localExceptionHandleAction);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual T? TryInline<T>(Func<T?> action, Action<Exception>? localExceptionHandleAction = null)
    {
        return Try(action, localExceptionHandleAction).Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual T TryEnsureInline<T>(Func<T> action, Action<Exception>? localExceptionHandleAction = null) where T : new()
    {
        return TryEnsure(action, localExceptionHandleAction).Value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual T TryFallBackInline<T>(in T fallbackValue, Func<T> action, Action<Exception>? localExceptionHandleAction = null)
    {
        return TryFallBack(fallbackValue, action, localExceptionHandleAction).Value;
    }
    #endregion
}
