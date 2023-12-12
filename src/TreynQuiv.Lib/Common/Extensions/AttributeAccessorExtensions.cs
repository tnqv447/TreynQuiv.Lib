using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace TreynQuiv.Lib.Common.Extensions;

public static class AttributeAccessorExtensions
{
    /// <summary>
    /// Get value of <see cref="DisplayNameAttribute"/> of this property
    /// </summary>
    /// <remarks>Return property name if there is no <see cref="DisplayNameAttribute"/> added to this property</remarks>
    /// <param name="model"></param>
    /// <param name="expression"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <returns>The display name, property name if <see cref="DisplayNameAttribute"/> not found or <c>null</c> if property not found.</returns>
    public static string? GetDisplayName<T, TProperty>(this T model, Expression<Func<T, TProperty>> expression)
    {
        var memberInfo = model.GetMemberInfo(expression);
        return memberInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? memberInfo.Name;
    }

    /// <summary>
    /// Get value of <see cref="DescriptionAttribute"/> of this property
    /// </summary>
    /// <remarks>Return property name if there is no <see cref="DescriptionAttribute"/> added to this property</remarks>
    /// <param name="model"></param>
    /// <param name="expression"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <returns>The description, property name if <see cref="DescriptionAttribute"/> not found or <see ref="null"/> if property not found.</returns>
    public static string? GetDescription<T, TProperty>(this T model, Expression<Func<T, TProperty>> expression)
    {
        var memberInfo = model.GetMemberInfo(expression);
        return memberInfo.GetCustomAttribute<DescriptionAttribute>()?.Description ?? memberInfo.Name;
    }

    /// <summary>
    /// Get <see cref="MemberInfo"/> of this property
    /// </summary>
    /// <param name="model"></param>
    /// <param name="expression"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <returns>The <see cref="MemberInfo"/> or <c>null</c> if not found property</returns>
    public static MemberInfo GetMemberInfo<T, TProperty>(this T model, Expression<Func<T, TProperty>> expression)
    {
        if (expression.Body is not MemberExpression memberExpression)
        {
            throw new ArgumentException($"The {nameof(expression)} is not a {nameof(MemberExpression)}");
        }

        return memberExpression.Member;
    }

    /// <summary>
    /// Retrieves a read-only collection of <typeparamref name="TAttribute"/> of a specified type that are applied to a specified member..
    /// </summary>
    /// <param name="memberInfo"></param>
    /// <typeparam name="TAttribute"></typeparam>
    /// <returns>A read-only collection of the custom attributes that are applied to <see langword="element"/> and that match <typeparamref name="TAttribute"/>, or an empty collection if no such attributes exist.</returns>
    public static IReadOnlyList<TAttribute> GetCustomAttributeList<TAttribute>(this MemberInfo memberInfo) where TAttribute : Attribute
    {
        return memberInfo.GetCustomAttributes<TAttribute>().ToList().AsReadOnly();
    }
}
