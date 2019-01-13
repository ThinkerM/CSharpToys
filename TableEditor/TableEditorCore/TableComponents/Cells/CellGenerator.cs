using TableEditorCore.Calculation;

namespace TableEditorCore.TableComponents.Cells
{
    internal static class CellGenerator
    {
        public static ICell CreateEmptyCell(Coordinates coordinates)
            => new EmptyCell(coordinates);

        public static ICell CreateErrorCell(Coordinates coordinates, string errorMessage)
            => new ErrorCell(coordinates, errorMessage);

        public static ICell CreateValueCell(Coordinates coordinates, int value)
            => new NumberCell(coordinates, value);

        public static ICell CreateFormulaCell(Coordinates coordinates, IBinaryFormula formula)
            => new FormulaCell(coordinates, formula);
    }
}
