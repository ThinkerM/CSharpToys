using System.Text.RegularExpressions;
using TableEditorCore.Parsing.Interfaces;
using TableEditorCore.TableComponents;

namespace TableEditorCore.Parsing
{
    internal class CoordinatesParser : ICoordinatesParser
    {
        /// <summary>
        /// Matches either regular coordinates ("AC1204", "XBDSA230", etc.) or sheet-prefixed coordinates ("Sheet2!AC12"). 
        /// </summary>
        private static readonly Regex CoordinatesPattern = new Regex(@"^(?:(\w+)!)?([A-Z]+)(\d+)$");

        /// <summary>
        /// Parse <see cref="Coordinates"/> from a string
        /// </summary>
        /// <param name="coordinatesString">String to parse coordinates from</param>
        /// <param name="coordinatesHomesheetName">Name of the sheet from which the coordinates are being parsed. 
        /// Used to specify the coordinates' homesheet value if they do not point to another sheet</param>
        /// <returns></returns>
        public Coordinates Parse(string coordinatesString, string coordinatesHomesheetName)
        {
            var match = CoordinatesPattern.Match(coordinatesString);
            if (!match.Success)
                return default(Coordinates);

            string sheetName = match.Groups[1].Value.IsNullOrEmpty()
                ? coordinatesHomesheetName
                : match.Groups[1].Value;
            string column = match.Groups[2].Value;
            int row = int.Parse(match.Groups[3].Value);
            return new Coordinates(column, row, sheetName);
        }
    }
}
