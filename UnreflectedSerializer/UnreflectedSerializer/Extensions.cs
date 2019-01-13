using System;
using System.IO;
using System.Linq.Expressions;
using UnreflectedSerializer.Mapping;

namespace UnreflectedSerializer
{
    internal static class Extensions
    {
        public static bool TryConvert<T>(this string input, out T converted)
        {
            try
            {
                converted = (T)Convert.ChangeType(input, typeof(T));
                return true;
            }
            catch(InvalidCastException)
            {
                converted = default(T);
                return false;
            }
        }

        public static Action<TSource, TValue> ToSetter<TSource, TValue>(this Expression<Func<TSource, TValue>> getExpression)
        {
            var entityParameterExpression = (ParameterExpression)((MemberExpression)getExpression.Body).Expression;
            var valueParameterExpression = Expression.Parameter(typeof(TValue));
            try
            {
                return Expression.Lambda<Action<TSource, TValue>>(
                        Expression.Assign(getExpression.Body, valueParameterExpression),
                        entityParameterExpression, valueParameterExpression)
                    .Compile();
            }
            catch (ArgumentException)
            {
                throw new MissingSetterException(typeof(TSource), getExpression.GetName());
            }
        }

        public static string GetName<TSource, TValue>(this Expression<Func<TSource, TValue>> property)
        {
            return (property.Body as MemberExpression ?? (MemberExpression)((UnaryExpression)property.Body).Operand).Member.Name;
        }

        public static void WriteLineOffset(this TextWriter writer, string value, int tabsOffset, OffsetType type = OffsetType.Tabs)
        {
            string offset = type == OffsetType.Tabs
                ? new string('\t', tabsOffset)
                : new string(' ', tabsOffset * 5);
            writer.WriteLine(offset + value);
        }

        public static string ToXmlOpeningTag(this string value) => $"<{value}>";

        public static string ToXmlClosingTag(this string value) => $"</{value}>";

        public static string RemoveXmlBrackets(this string xmlTag) => xmlTag?.TrimStart('<', '/').TrimEnd('>');
    }
    internal enum OffsetType { Tabs, Spaces }
}