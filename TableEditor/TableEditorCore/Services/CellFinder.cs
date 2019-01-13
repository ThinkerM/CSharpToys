using System.IO;
using TableEditorCore.Parsing.Interfaces;
using TableEditorCore.TableComponents;
using TableEditorCore.TableComponents.Cells;

namespace TableEditorCore.Services
{
    internal class CellFinder : ICellFinder
    {
        private readonly SheetRepository repository;

        public CellFinder(SheetRepository sheetRepository)
        {
            this.repository = sheetRepository;
        }

        public ICell Find(Coordinates coordinates)
        {
            ISheet targetSheet = repository.Find(coordinates.SheetName);

            return targetSheet?[coordinates];
        }
    }
}
