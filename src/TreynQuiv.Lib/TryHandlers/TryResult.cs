namespace TreynQuiv.Lib.TryHandlers;

public abstract class TryResult(bool success = false)
{
    public bool Success { get; } = success;

    public static implicit operator bool(TryResult tryResult)
    {
        return tryResult.Success;
    }
}

/// <summary>
/// Contains return result <typeparamref name="T"/> after using <see cref="TryHandler"/> or <see cref="TryAsyncHandler"/>
/// </summary>
/// <remarks>Can be used as <see cref="bool"/> with value from wether the try block execution successful or not</remarks>
/// <typeparam name="T"></typeparam>
public class TryResult<T>(bool success, T? value) : TryResult(success)
{
    public T? Value { get; } = value;
}

/// <summary>
/// Contains return non-nullable result <typeparamref name="T"/> after using <see cref="TryHandler"/> or <see cref="TryAsyncHandler"/>
/// </summary>
/// <remarks>Can be used as <see cref="bool"/> with value from wether the try block execution successful or not</remarks>
/// <typeparam name="T"></typeparam>
public class TryEnsuredResult<T>(bool success, T value) : TryResult(success)
{
    public T Value { get; } = value;
}

