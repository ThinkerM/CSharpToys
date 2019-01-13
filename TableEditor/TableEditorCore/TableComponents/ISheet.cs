using System.Collections.Generic;
using System.IO;
using TableEditorCore.Services;
using TableEditorCore.TableComponents.Cells;

namespace TableEditorCore.TableComponents
{
    public interface ISheet
    {
        string Name { get; }

        void AddCell(ICell cell);

        ICell this[Coordinates coordinates] { get; }

        void DescribeSelf(TextWriter outputTarget);

        void CalculateContentData(ICellEvaluator evaluator);
    }
}
