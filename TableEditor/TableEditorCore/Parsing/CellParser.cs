using TableEditorCore.Calculation;
using TableEditorCore.Parsing.Interfaces;
using TableEditorCore.TableComponents;
using TableEditorCore.TableComponents.Cells;

namespace TableEditorCore.Parsing
{
    internal class CellParser : ICellParser
    {
        private readonly BinaryFormulaParser formulaParser;

        public CellParser(BinaryFormulaParser formulaParser)
        {
            this.formulaParser = formulaParser;
        }

        public ICell Parse(string cellString, Coordinates cellCoordinates)
        {
            if (cellString == "[]")
            {
                return CellGenerator.CreateEmptyCell(cellCoordinates);
            }

            int cellValue;
            if (int.TryParse(cellString, out cellValue))
            {
                return CellGenerator.CreateValueCell(cellCoordinates, cellValue);
            }

            if (!cellString.StartsWith(ParsingConstants.FORMULA_INITIAL_TOKEN))
            {
                return CellGenerator.CreateErrorCell(cellCoordinates, ParsingConstants.INVALID_VALUE);
            }

            try
            {
                IBinaryFormula cellFormula = formulaParser.Parse(cellString, cellCoordinates.SheetName);
                return CellGenerator.CreateFormulaCell(cellCoordinates, cellFormula);
            }
            catch (MissingOperatorException)
            {
                return CellGenerator.CreateErrorCell(cellCoordinates, ParsingConstants.MISSING_OPERATOR);
            }
            catch (FormulaFormatException)
            {
                return CellGenerator.CreateErrorCell(cellCoordinates, ParsingConstants.FORMULA_SYNTAX_ERROR);
            }
        }
    }
}
