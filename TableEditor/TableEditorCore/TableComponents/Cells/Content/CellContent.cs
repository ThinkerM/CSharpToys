using TableEditorCore.Calculation;

namespace TableEditorCore.TableComponents.Cells.Content
{
    internal class CellContent : ICellContent
    {
        public CellContent(int value)
        {
            Value = value;
            ErrorType = ErrorType.None;
        }

        public CellContent(ErrorType error)
        {
            Value = 0;
            ErrorType = error;
            IsEvaluated = true;
        }

        public int Value { get; }
        public bool Valid => ErrorType == ErrorType.None;
        public bool IsEvaluated { get; private set; } = false;
        public ErrorType ErrorType { get; }

        public ICellContent Evaluated()
        {
            IsEvaluated = true;
            return this;
        }

        public string View
            => Valid
                ? Value.ToString()
                : ErrorType.ToStringRepresentation();

        public static readonly ICellContent Empty = new CellContent(0);
    }
}
