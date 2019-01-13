using TableEditorCore.Calculation;

namespace TableEditorCore.TableComponents.Cells
{
    public interface ICellContent
    {
        /// <summary>
        /// Numerical value stored within the cell
        /// </summary>
        int Value { get; }

        /// <summary>
        /// Signals that the content is valid for calculations
        /// </summary>
        bool Valid { get; }

        /// <summary>
        /// Indicates whether the content has passed an evaluation process
        /// </summary>
        bool IsEvaluated { get; }

        /// <summary>
        /// Underlying error cause
        /// </summary>
        ErrorType ErrorType { get; }

        /// <summary>
        /// Mark the content as evaluated
        /// </summary>
        ICellContent Evaluated();

        string View { get; }
    }
}