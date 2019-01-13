using TableEditorCore.TableComponents;
using TableEditorCore.TableComponents.Cells;

namespace TableEditorCore.Services
{
    public interface ICellFinder
    {
        ICell Find(Coordinates coordinates);
    }
}
