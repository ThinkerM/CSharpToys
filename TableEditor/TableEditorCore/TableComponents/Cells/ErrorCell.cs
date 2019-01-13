using TableEditorCore.Calculation;
using TableEditorCore.TableComponents.Cells.Content;

namespace TableEditorCore.TableComponents.Cells
{
    internal class ErrorCell : StaticCell
    {
        public ErrorCell(Coordinates coordinates, string errorMessage) : base(coordinates)
        {
            View = errorMessage;
        }

        public override ICellContent Data { get; set; } = new ErrorContent(CellContent.Empty, ErrorType.DataError);

        public override string View { get; }
    }
}
