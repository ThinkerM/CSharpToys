using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TableEditorCore.Parsing.Interfaces;
using TableEditorCore.TableComponents;

namespace TableEditorCore.Parsing
{
    internal class NonRegexCoordinatesParser : ICoordinatesParser
    {
        public Coordinates Parse(string coordinatesString, string coordinatesHomesheetName)
        {
            var sheetSplit = coordinatesString.Split(ParsingConstants.SHEET_MARKER);

            if (sheetSplit.Length > 2)
                return default(Coordinates);

            if (sheetSplit.Length == 2)
            {
                return ParseCoordinatesWithSheet(sheetSplit[0], sheetSplit[1]);
            }
            return ParseHomesheetCoordinates(coordinatesString, coordinatesHomesheetName);
        }

        private Coordinates ParseCoordinatesWithSheet(string sheetName, string coordinatesStringRemainder)
        {
            var coordinates = ParsePureCoordinates(coordinatesStringRemainder);
            return coordinates == default(Coordinates)
                ? coordinates
                : new Coordinates(coordinates.Column, coordinates.Row, sheetName);
        }

        private Coordinates ParseHomesheetCoordinates(string coordinatesString, string homeSheetName)
        {
            var coordinates = ParsePureCoordinates(coordinatesString);
            return coordinates == default(Coordinates)
                ? coordinates
                : new Coordinates(coordinates.Column, coordinates.Row, homeSheetName);
        }

        private static Coordinates ParsePureCoordinates(string coordinatesString)
        {
            if (!char.IsDigit(coordinatesString.Last()) //sanity check
                || !char.IsUpper(coordinatesString.First()))
            {
                return default(Coordinates);
            }

            string column = ReadWhile(ref coordinatesString, char.IsUpper);
            //coordinatesString = coordinatesString.Substring(column.Length);
            string rowString = ReadWhile(ref coordinatesString, char.IsDigit);

            if (column.IsNullOrEmpty() || rowString.IsNullOrEmpty())
            {
                return default(Coordinates);
            }

            return new Coordinates(column, int.Parse(rowString), null);
        }

        private static string ReadWhile(ref string value, Predicate<char> condition)
        {
            var resultString = new StringBuilder();
            for (int i = 0; i < value.Length && condition(value[i]); i++)
            {
                resultString.Append(value[i]);
            }
            value = value.Substring(resultString.Length);
            return resultString.ToString();
        }
    }
}
