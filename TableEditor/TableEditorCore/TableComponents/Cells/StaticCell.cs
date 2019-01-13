using TableEditorCore.Calculation;

namespace TableEditorCore.TableComponents.Cells
{
    /// <summary>
    /// Base class for cells whose content cannot change (no formula to dynamically force content change)
    /// </summary>
    internal abstract class StaticCell : ICell
    {
        protected StaticCell(Coordinates coordinates)
        {
            Coordinates = coordinates;
        }

        public Coordinates Coordinates { get; }

        public abstract ICellContent Data { get; set; }

        public abstract string View { get; }

        public bool BeingEvaluated
        {
            get { return false; }
            set { } //bit of a hack, could use a cleaner solution (static cells are never "being evaluated"
        }

        public IBinaryFormula Formula { get; } = null;
    }
}
