using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TreynQuiv.Lib.Common.Extensions;

public static class ExpressionExtensions
{
    /// <summary>
    /// Creates a <see cref="Expression{Func{T, bool}}"/> that represents a conditional <see langword="AND"/> operation that evaluates the second operand only if the first operand evaluates to <see langword="true"/>.
    /// </summary>
    /// <returns>
    /// A <see cref="Expression{Func{T, bool}}"/> that has the <see cref="Expression.NodeType"/> property equal to <see cref="ExpressionType.AndAlso"/> and the <see cref="BinaryExpression.Left"/> and <see cref="BinaryExpression.Right"/> properties set to the specified values.
    /// </returns>
    public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
    {
        var parameter = Expression.Parameter(typeof(T), "x");

        var leftVisitor = new ReplaceExpressionVisitor(left.Parameters[0], parameter);
        var leftExp = leftVisitor.Visit(left.Body)!;

        var rightVisitor = new ReplaceExpressionVisitor(right.Parameters[0], parameter);
        var rightExp = rightVisitor.Visit(right.Body)!;

        return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(leftExp, rightExp), parameter);
    }

    /// <summary>
    /// Creates a <see cref="Expression{Func{T, bool}}"/> that represents a conditional <see langword="OR"/> operation that evaluates the second operand only if the first operand evaluates to <see langword="false"/>.
    /// </summary>
    /// <returns>
    /// A <see cref="Expression{Func{T, bool}}"/> that has the <see cref="Expression.NodeType"/> property equal to <see cref="ExpressionType.OrElse"/> and the <see cref="BinaryExpression.Left"/> and <see cref="BinaryExpression.Right"/> properties set to the specified values.
    /// </returns>
    public static Expression<Func<T, bool>> OrElse<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
    {
        var parameter = Expression.Parameter(typeof(T), "x");

        var leftVisitor = new ReplaceExpressionVisitor(left.Parameters[0], parameter);
        var leftExp = leftVisitor.Visit(left.Body)!;

        var rightVisitor = new ReplaceExpressionVisitor(right.Parameters[0], parameter);
        var rightExp = rightVisitor.Visit(right.Body)!;

        return Expression.Lambda<Func<T, bool>>(Expression.OrElse(leftExp, rightExp), parameter);
    }

    private class ReplaceExpressionVisitor(Expression oldValue, Expression newValue) : ExpressionVisitor
    {
        private readonly Expression _oldValue = oldValue;
        private readonly Expression _newValue = newValue;

        public override Expression? Visit(Expression? node)
        {
            return node == _oldValue ? _newValue : base.Visit(node);
        }
    }
}
