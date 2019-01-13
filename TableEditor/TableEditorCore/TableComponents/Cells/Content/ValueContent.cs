using TableEditorCore.Calculation;

namespace TableEditorCore.TableComponents.Cells.Content
{
    internal class ValueContent : CellContentDecorator
    {
        public ValueContent(ICellContent content, int value) : base(content)
        {
            this.value = value;
            this.errorType = ErrorType.None;
            this.evaluated = true;
        }
    }
}
