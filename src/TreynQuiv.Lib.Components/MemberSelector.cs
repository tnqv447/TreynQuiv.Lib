using System.Linq.Expressions;

namespace TreynQuiv.Lib.Components;

public class MemberSelector<T>
{
    public LambdaExpression Expression { get; init; }
    public Delegate Selector { get => Expression.Compile(); }
    public MemberExpression MemberExpression { get; init; }
    public Type MemberType { get => Expression.ReturnType; }
    public MemberSelector(LambdaExpression selector)
    {
        if (selector.Body is not MemberExpression memberExpression)
        {
            throw new InvalidDataException($"The {nameof(selector)} is not a {nameof(MemberExpression)}");
        }

        Expression = selector;
        MemberExpression = memberExpression;
    }
}
