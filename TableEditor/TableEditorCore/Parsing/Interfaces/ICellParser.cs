using TableEditorCore.TableComponents;
using TableEditorCore.TableComponents.Cells;

namespace TableEditorCore.Parsing.Interfaces
{
    internal interface ICellParser
    {
        /// <summary>
        /// Parse a cell string which contains the cell's actual data but no reference to its coordinates 
        /// (coordinates must be inferred from outside context)
        /// </summary>
        /// <param name="cellString">String containing information about the cell's internal data</param>
        /// <param name="cellCoordinates">Coordinates gained from context outside of the cell string itself</param>
        /// <returns></returns>
        ICell Parse(string cellString, Coordinates cellCoordinates);
    }
}