using System.Runtime.CompilerServices;

namespace TranQuiv.Lib.TryHandlers;

/// <summary>
/// Reusable and short hand for try catch block to execute asynchronous functions.
/// </summary>
public class TryAsyncHandler
{
    public Func<Task>? BeforeTask { get; init; }
    public Func<Task>? AfterTask { get; init; }
    public required Func<Exception, Task> ExceptionHandleTask { get; init; }
    public Func<Task>? FinallyTask { get; init; }

    public virtual async Task<bool> TryAsync(Func<Task> funcTask, Func<Exception, Task>? localExceptionHandleTask = null)
    {
        try
        {
            await (BeforeTask?.Invoke() ?? Task.CompletedTask);
            await funcTask.Invoke();
            await (AfterTask?.Invoke() ?? Task.CompletedTask);
            return true;
        }
        catch (Exception ex)
        {
            await ExceptionHandleTask.Invoke(ex);
            await (localExceptionHandleTask?.Invoke(ex) ?? Task.CompletedTask);
        }
        finally
        {
            await (FinallyTask?.Invoke() ?? Task.CompletedTask);
        }

        return false;
    }

    public virtual async Task<TryResult<T>> TryAsync<T>(Func<Task<T?>> funcTask, Func<Exception, Task>? localExceptionHandleTask = null)
    {
        try
        {
            await (BeforeTask?.Invoke() ?? Task.CompletedTask);
            var result = await funcTask.Invoke();
            await (AfterTask?.Invoke() ?? Task.CompletedTask);

            return new(true, result);
        }
        catch (Exception ex)
        {
            await ExceptionHandleTask.Invoke(ex);
            await (localExceptionHandleTask?.Invoke(ex) ?? Task.CompletedTask);
        }
        finally
        {
            await (FinallyTask?.Invoke() ?? Task.CompletedTask);
        }

        return new(false, default);
    }

    public virtual async Task<TryEnsuredResult<T>> TryEnsureAsync<T>(Func<Task<T?>> funcTask, Func<Exception, Task>? localExceptionHandleTask = null) where T : new()
    {
        var result = await TryAsync(funcTask, localExceptionHandleTask);
        return result.IsSuccess ? new(true, result.Result ?? new()) : new(false, new());
    }

    public virtual async Task<TryEnsuredResult<T>> TryFallbackAsync<T>(T fallbackValue, Func<Task<T?>> funcTask, Func<Exception, Task>? localExceptionHandleTask = null)
    {
        var result = await TryAsync(funcTask, localExceptionHandleTask);
        return result.IsSuccess ? new(true, result.Result ?? fallbackValue) : new(false, fallbackValue);
    }

    #region Non-Async
    public virtual bool Try(Action action, Func<Exception>? localExceptionHandleAction = null)
    {
        return TryAsync(
            () => Task.Run(action),
            (ex) => localExceptionHandleAction is null
                ? Task.CompletedTask
                : Task.Run<Exception>(localExceptionHandleAction)).GetAwaiter().GetResult();
    }

    public virtual TryResult<T> Try<T>(Func<T?> action, Func<Exception>? localExceptionHandleAction = null)
    {
        return TryAsync(
            () => Task.Run(action),
            (ex) => localExceptionHandleAction is null
                ? Task.CompletedTask
                : Task.Run<Exception>(localExceptionHandleAction)).GetAwaiter().GetResult();
    }

    public virtual TryEnsuredResult<T> TryEnsure<T>(Func<T?> action, Func<Exception>? localExceptionHandleAction = null) where T : new()
    {
        return TryEnsureAsync(
            () => Task.Run(action),
            (ex) => localExceptionHandleAction is null
                ? Task.CompletedTask
                : Task.Run<Exception>(localExceptionHandleAction)).GetAwaiter().GetResult();
    }

    public virtual TryEnsuredResult<T> TryFallback<T>(T fallbackValue, Func<T?> action, Func<Exception>? localExceptionHandleAction = null)
    {
        return TryFallbackAsync(
            fallbackValue,
            () => Task.Run(action),
            (ex) => localExceptionHandleAction is null
                ? Task.CompletedTask
                : Task.Run<Exception>(localExceptionHandleAction)).GetAwaiter().GetResult();
    }
    #endregion

    #region Inline-Async

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual async Task TryInlineAsync(Func<Task> funcTask, Func<Exception, Task>? localExceptionHandleAction = null)
    {
        _ = await TryAsync(funcTask, localExceptionHandleAction);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual async Task<T?> TryInlineAsync<T>(Func<Task<T?>> funcTask, Func<Exception, Task>? localExceptionHandleTask = null)
    {
        return (await TryAsync(funcTask, localExceptionHandleTask)).Result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual async Task<T> TryEnsureInlineAsync<T>(Func<Task<T?>> funcTask, Func<Exception, Task>? localExceptionHandleTask = null) where T : new()
    {
        return (await TryEnsureAsync(funcTask, localExceptionHandleTask)).Result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual async Task<T> TryFallbackInlineAsync<T>(T fallbackValue, Func<Task<T?>> funcTask, Func<Exception, Task>? localExceptionHandleTask = null) where T : new()
    {
        return (await TryFallbackAsync(fallbackValue, funcTask, localExceptionHandleTask)).Result;
    }
    #endregion

    #region Inline-Non-Async

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual void TryInline(Action action, Func<Exception>? localExceptionHandleAction = null)
    {
        _ = Try(action, localExceptionHandleAction);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual T? TryInline<T>(Func<T?> action, Func<Exception>? localExceptionHandleAction = null)
    {
        return Try(action, localExceptionHandleAction).Result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual T TryEnsureInline<T>(Func<T?> action, Func<Exception>? localExceptionHandleAction = null) where T : new()
    {
        return TryEnsure(action, localExceptionHandleAction).Result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual T TryFallbackInline<T>(T fallbackValue, Func<T?> function, Func<Exception>? localExceptionHandleAction = null)
    {
        return TryFallback(fallbackValue, function, localExceptionHandleAction).Result;
    }
    #endregion
}

