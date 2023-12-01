using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace TranQuiv.Lib.Common.Extensions;

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
        MemberExpression memberExpression = (MemberExpression)expression.Body;
        string? propertyName = (memberExpression.Member is PropertyInfo propertyInfo) ? propertyInfo.Name : null;
        if (string.IsNullOrEmpty(propertyName))
        {
            return null;
        }

        return model?.GetType().GetMember(propertyName).FirstOrDefault()
            ?.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? propertyName;
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
        MemberExpression memberExpression = (MemberExpression)expression.Body;
        string? propertyName = (memberExpression.Member is PropertyInfo propertyInfo) ? propertyInfo.Name : null;
        if (string.IsNullOrEmpty(propertyName))
        {
            return null;
        }

        return model?.GetType().GetMember(propertyName).FirstOrDefault()
            ?.GetCustomAttribute<DescriptionAttribute>()?.Description ?? propertyName;
    }

    /// <summary>
    /// Get <see cref="MemberInfo"/> of this property
    /// </summary>
    /// <param name="model"></param>
    /// <param name="expression"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <returns>The <see cref="MemberInfo"/> or <c>null</c> if not found property</returns>
    public static MemberInfo? GetMemberInfo<T, TProperty>(this T model, Expression<Func<T, TProperty>> expression)
    {
        MemberExpression memberExpression = (MemberExpression)expression.Body;
        string? propertyName = (memberExpression.Member is PropertyInfo propertyInfo) ? propertyInfo.Name : null;
        if (string.IsNullOrEmpty(propertyName))
        {
            return null;
        }

        return model?.GetType().GetMember(propertyName).FirstOrDefault();
    }

    /// <summary>
    /// Get <typeparamref name="TAttribute"/> of this <see cref="MemberInfo"/>.
    /// </summary>
    /// <param name="memberInfo"></param>
    /// <typeparam name="TAttribute"></typeparam>
    /// <returns>The <typeparamref name="TAttribute"/> or <c>null</c> if not found</returns>
    public static TAttribute? GetAttribute<TAttribute>(this MemberInfo memberInfo) where TAttribute : Attribute
    {
        return memberInfo.GetCustomAttribute<TAttribute>();
    }

    /// <summary>
    /// Get a list of <typeparamref name="TAttribute"/> of this <see cref="MemberInfo"/>.
    /// </summary>
    /// <param name="memberInfo"></param>
    /// <typeparam name="TAttribute"></typeparam>
    /// <returns>A list of <typeparamref name="TAttribute"/>. Empty if not found</returns>
    public static IReadOnlyList<TAttribute> GetAttributes<TAttribute>(this MemberInfo memberInfo) where TAttribute : Attribute
    {
        return memberInfo.GetCustomAttributes<TAttribute>().ToList().AsReadOnly();
    }
}
