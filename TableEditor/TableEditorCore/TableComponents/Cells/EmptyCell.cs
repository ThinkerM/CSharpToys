using TableEditorCore.Calculation;
using TableEditorCore.TableComponents.Cells.Content;

namespace TableEditorCore.TableComponents.Cells
{
    internal class EmptyCell : StaticCell
    {
        public EmptyCell(Coordinates coordinates) : base(coordinates) { }

        public override ICellContent Data { get; set; } = new ValueContent(CellContent.Empty, 0);

        public override string View => "[]";
    }
}
