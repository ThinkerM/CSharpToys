using System.Collections.Generic;
using System.IO;
using TableEditorCore.TableComponents;
using TableEditorCore.TableComponents.Cells;

namespace TableEditorCore.Parsing.Interfaces
{
    internal interface ISheetParser
    {
        ISheet Parse(TextReader sheetFile, string sheetName);

        ICell ParseAtCoordinates(TextReader sheetFileReader, Coordinates coordinates);
    }
}