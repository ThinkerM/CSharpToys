using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace TableEditorCore
{
    public static class Extensions
    {
        [DebuggerStepThrough]
        public static string ToExcelColumn(this int columnNumber)
        {
            if (columnNumber < 1)
                throw new ArgumentOutOfRangeException(nameof(columnNumber));

            if (columnNumber <= 26)
            {
                return Convert.ToChar(columnNumber + 64).ToString();
            }
            int div = columnNumber / 26;
            int mod = columnNumber % 26;
            if (mod == 0)
            {
                mod = 26;
                div--;
            }
            return ToExcelColumn(div) + ToExcelColumn(mod);
        }

        [DebuggerStepThrough]
        public static long AsOrdinalNumber(this string excelColumnAdress)
        {
            int[] digits = new int[excelColumnAdress.Length];
            for (int i = 0; i < excelColumnAdress.Length; i++)
            {
                digits[i] = Convert.ToInt32(char.ToUpper(excelColumnAdress[i])) - 64;
            }
            int mul = 1;
            int res = 0;
            for (int position = digits.Length - 1; position >= 0; position--)
            {
                res += digits[position] * mul;
                mul *= 26;
            }
            return res;
        }

        [DebuggerStepThrough]
        public static bool IsNullOrWhitespace(this string value)
            => string.IsNullOrWhiteSpace(value);

        [DebuggerStepThrough]
        public static bool IsNullOrEmpty(this string value)
            => string.IsNullOrEmpty(value);

        [DebuggerStepThrough]
        public static string RemoveEndIfExists(this string value, string toRemove)
        {
            return value.EndsWith(toRemove)
                ? value.Remove(value.Length - toRemove.Length)
                : value;
        }

        [DebuggerStepThrough]
        public static string AddEndIfNotExists(this string value, string endToAdd)
        {
            return value.EndsWith(endToAdd)
                ? value
                : value + endToAdd;
        }

        public static void PushMultiple<T>(this Stack<T> stack, IEnumerable<T> toPush, Expression<Func<T, bool>> filter)
        {
            foreach (T filteredItem in toPush.AsQueryable().Where(filter))
            {
                stack.Push(filteredItem);
            }
        }
    }
}
