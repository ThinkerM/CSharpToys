using TableEditorCore.Calculation;

namespace TableEditorCore.TableComponents.Cells.Content
{
    internal abstract class CellContentDecorator : ICellContent
    {
        protected int value;
        protected bool evaluated;
        protected ErrorType errorType;

        protected CellContentDecorator(ICellContent content)
        {
            value = content.Value;
            evaluated = content.IsEvaluated;
            errorType = content.ErrorType;
        }

        public int Value => value;

        public bool Valid => errorType == ErrorType.None;

        public bool IsEvaluated => evaluated;

        public ErrorType ErrorType => errorType;

        public ICellContent Evaluated()
        {
            evaluated = true;
            return this;
        }

        public string View
            => Valid
                ? value.ToString()
                : errorType.ToStringRepresentation();
    }
}
