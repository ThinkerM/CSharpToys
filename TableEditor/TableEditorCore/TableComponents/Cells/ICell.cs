using TableEditorCore.Calculation;

namespace TableEditorCore.TableComponents.Cells
{
    public interface ICell
    {
        Coordinates Coordinates { get; }

        ICellContent Data { get; set; }

        string View { get; }

        bool BeingEvaluated { get; set; }

        IBinaryFormula Formula { get; }
    }
}
