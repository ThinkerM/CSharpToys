using TableEditorCore.Calculation;

namespace TableEditorCore.TableComponents.Cells.Content
{
    internal class ErrorContent : CellContentDecorator
    {
        public ErrorContent(ICellContent content, ErrorType error) : base(content)
        {
            this.errorType = error;
            this.evaluated = true;
        }
    }
}
