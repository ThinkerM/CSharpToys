using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TableEditorCore.Parsing.Interfaces;
using TableEditorCore.Services;

namespace TableEditorCore.TableComponents
{
    internal class SheetRepository
    {
        private readonly ISheetParser sheetParser;
        private readonly IDictionary<string, ISheet> knownSheets = new Dictionary<string, ISheet>();

        public SheetRepository(ISheetParser sheetParser)
        {
            this.sheetParser = sheetParser;
        }

        public void Add(ISheet sheet)
        {
            knownSheets.Add(sheet.Name, sheet);
        }

        public ISheet Find(string sheetName)
        {
            return knownSheets.ContainsKey(sheetName)
                ? knownSheets[sheetName]
                : ParseNewSheet(sheetName);
        }

        private ISheet ParseNewSheet(string sheetName)
        {
            TextReader sheetFile;
            ISheet parsedSheet;
            if (FileLoader.TryOpenFileText(sheetName.AddEndIfNotExists(ComponentsConstants.SHEET_EXTENSION),
                                           out sheetFile))
            {
                parsedSheet = sheetParser.Parse(sheetFile, sheetName);
            }
            else
            {
                parsedSheet = null;
            }

            knownSheets.Add(sheetName, parsedSheet);
            return parsedSheet;
        }
    }
}
