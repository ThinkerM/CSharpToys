using TableEditorCore.TableComponents;

namespace TableEditorCore.Parsing.Interfaces
{
    interface ICoordinatesParser
    {
        Coordinates Parse(string coordinatesString, string coordinatesHomesheetName);
    }
}
