namespace TranQuiv.Lib.TryHandlers;

public abstract class TryResult
{
    public bool IsSuccess { get; set; }
    public TryResult() { }
    public TryResult(bool isSuccess) { IsSuccess = isSuccess; }

    public static implicit operator bool(TryResult tryResult)
    {
        return tryResult.IsSuccess;
    }
}

/// <summary>
/// Contains return result <typeparamref name="T"/> after using <see cref="TryHandler"/> or <see cref="TryAsyncHandler"/>
/// </summary>
/// <remarks>Can be used as <see cref="bool"/> with value from wether the try block execution successful or not</remarks>
/// <typeparam name="T"></typeparam>
public class TryResult<T> : TryResult
{
    public T? Result { get; set; }

    public TryResult(bool isSuccess, T? result) : base(isSuccess)
    {
        Result = result;
    }

    internal void Deconstruct(out bool isSuccess, out T? result)
    {
        isSuccess = IsSuccess;
        result = Result;
    }
}

/// <summary>
/// Contains return non-nullable result <typeparamref name="T"/> after using <see cref="TryHandler"/> or <see cref="TryAsyncHandler"/>
/// </summary>
/// <remarks>Can be used as <see cref="bool"/> with value from wether the try block execution successful or not</remarks>
/// <typeparam name="T"></typeparam>
public class TryEnsuredResult<T> : TryResult
{
    public T Result { get; set; }

    public TryEnsuredResult(bool isSuccess, T result) : base(isSuccess)
    {
        Result = result;
    }

    internal void Deconstruct(out bool isSuccess, out T result)
    {
        isSuccess = IsSuccess;
        result = Result;
    }
}

