using TableEditorCore.Calculation;
using TableEditorCore.TableComponents.Cells.Content;

namespace TableEditorCore.TableComponents.Cells
{
    internal class FormulaCell : ICell
    {
        /// <summary>
        /// Create an instance of a cell which contains an <see cref="BinaryFormula"/>
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="formula"></param>
        public FormulaCell(Coordinates coordinates, IBinaryFormula formula)
        {
            Coordinates = coordinates;
            Formula = formula;
        }

        public Coordinates Coordinates { get; }

        public ICellContent Data { get; set; } = CellContent.Empty;

        public string View => Data.View;

        public IBinaryFormula Formula { get; }

        public bool BeingEvaluated { get; set; }
    }
}
