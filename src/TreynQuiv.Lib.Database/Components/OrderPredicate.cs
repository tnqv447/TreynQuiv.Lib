using System.Linq.Expressions;

namespace TreynQuiv.Lib.Database.Components;

public record class OrderPredicate<T>
{
    public Expression<Func<T, dynamic>> MemberExpression { get; init; }
    public Func<T, dynamic> MemberSelector { get => MemberExpression.Compile(); }
    public bool Descending { get; init; }
    public OrderPredicate(Expression<Func<T, dynamic>> memberExpression, bool descending = false)
    {
        if (memberExpression.Body is not MemberExpression _)
        {
            throw new InvalidDataException($"{nameof(memberExpression)} is not {nameof(MemberExpression)}");
        }
        MemberExpression = memberExpression;
        Descending = descending;
    }
}
