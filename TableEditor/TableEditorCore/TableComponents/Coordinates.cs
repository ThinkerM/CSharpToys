using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TableEditorCore.TableComponents
{
    public struct Coordinates : IEquatable<Coordinates>
    {
        public Coordinates(string column, int row, string sheetName)
        {
            Column = column.ToUpper();
            Row = row;
            SheetName = sheetName?.RemoveEndIfExists(ComponentsConstants.SHEET_EXTENSION);
        }

        public string SheetName { get; }

        public string Column { get; }

        public int Row { get; }

        #region IEquatable Boilerplate
        [DebuggerStepThrough]
        public static bool operator == (Coordinates c1, Coordinates c2)
        {
            return (c1.Column == c2.Column
                    && c1.Row == c2.Row
                    && c1.SheetName == c2.SheetName);
        }

        [DebuggerStepThrough]
        public static bool operator != (Coordinates c1, Coordinates c2)
        {
            return !(c1 == c2);
        }

        [DebuggerStepThrough]
        public bool Equals(Coordinates other)
        {
            return this == other;
        }

        [DebuggerStepThrough]
        public override int GetHashCode()
        {
            var hashCode = 625011893;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SheetName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Column);
            hashCode = hashCode * -1521134295 + Row.GetHashCode();
            return hashCode;
        }

        [DebuggerStepThrough]
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        #endregion

        public override string ToString()
            => SheetName.IsNullOrEmpty()
                ? $"{Column}{Row}"
                : $"{SheetName}!{Column}{Row}";

        
    }
}
