using TableEditorCore.TableComponents.Cells.Content;

namespace TableEditorCore.TableComponents.Cells
{
    internal class NumberCell : StaticCell
    {
        /// <summary>
        /// Create an instance of a cell which contains a constant value
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="value"></param>
        public NumberCell(Coordinates coordinates, int value) : base(coordinates)
        {
            Data = new ValueContent(CellContent.Empty, value);
        }

        public sealed override ICellContent Data { get; set; }

        public override string View => Data.View;
    }
}
