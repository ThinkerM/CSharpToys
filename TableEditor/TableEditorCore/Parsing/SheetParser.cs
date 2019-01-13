using System;
using System.Collections.Generic;
using System.IO;
using TableEditorCore.Parsing.Interfaces;
using TableEditorCore.TableComponents;
using TableEditorCore.TableComponents.Cells;

namespace TableEditorCore.Parsing
{
    internal class SheetParser : ISheetParser
    {
        private readonly ICellParser cellParser;

        public SheetParser(ICellParser cellParser)
        {
            this.cellParser = cellParser;
        }

        public ISheet Parse(TextReader sheetFile, string sheetName)
        {
            var resultSheet = new Sheet(sheetName);
            int rowNumber = 1;
            string row;
            while ((row = sheetFile.ReadLine()) != null)
            {
                foreach (ICell cell in ParseRow(row, rowNumber, sheetName))
                {
                    resultSheet.AddCell(cell);
                }
                rowNumber++;
            }
            return resultSheet;
        }

        private IEnumerable<ICell> ParseRow(string row, int rowNumber, string sheetName)
        {
            var cellStrings = row.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < cellStrings.Length; i++)
            {
                yield return cellParser.Parse(cellStrings[i], new Coordinates((i + 1).ToExcelColumn(), rowNumber, sheetName));
            }
        }

        public ICell ParseAtCoordinates(TextReader sheetFileReader, Coordinates coordinates)
        {
            int rowsToSkip = coordinates.Row - 1;
            for (int i = 0; i < rowsToSkip && sheetFileReader.Peek() != -1; i++)
            {
                sheetFileReader.ReadLine();
            }
            string targetRow = sheetFileReader.ReadLine();

            var cellStrings = targetRow?.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (cellStrings != null && coordinates.Column.AsOrdinalNumber() - 1 < cellStrings.Length)
            {
                return cellParser.Parse(cellStrings[coordinates.Column.AsOrdinalNumber() - 1], coordinates);
            }
            return CellGenerator.CreateEmptyCell(coordinates);
        }
    }
}
