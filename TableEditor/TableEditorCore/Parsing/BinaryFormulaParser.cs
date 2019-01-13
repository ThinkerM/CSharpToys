using System.Linq;
using TableEditorCore.Calculation;
using TableEditorCore.Parsing.Interfaces;
using TableEditorCore.TableComponents;

namespace TableEditorCore.Parsing
{
    internal class BinaryFormulaParser : IBinaryFormulaParser
    {
        private static readonly char[] PossibleOperatorSymbols = { '+', '-', '*', '/' };

        private readonly ICoordinatesParser coordinatesParser;

        public BinaryFormulaParser(ICoordinatesParser coordinatesParser)
        {
            this.coordinatesParser = coordinatesParser;
        }

        /// <summary>
        /// Parse a formula from a given string or throw an appropriate exception if the string's format doesn't match the expected pattern
        /// </summary>
        /// <param name="formulaString"></param>
        /// <param name="parsedHomesheetName"></param>
        /// <returns></returns>
        public IBinaryFormula Parse(string formulaString, string parsedHomesheetName)
        {
            formulaString = formulaString.TrimStart(ParsingConstants.FORMULA_INITIAL_TOKEN.ToCharArray());

            char formulaOperator = ParseOperator(formulaString);

            return ComposeOperands(formulaString, formulaOperator, parsedHomesheetName);
        }

        private static char ParseOperator(string formulaString)
        {
            var operatorsInFormula = PossibleOperatorSymbols
                .Where(formulaString.Contains)
                .Select(op => op).ToArray();

            //check for exactly one operator being present
            if (operatorsInFormula.Length == 0)
                throw new MissingOperatorException();
            if (operatorsInFormula.Length > 1)
                throw new FormulaFormatException();

            return operatorsInFormula[0];
        }

        private IBinaryFormula ComposeOperands(string formulaString, char formulaOperator, string parsedHomesheetName)
        {
            string[] operandCandidates = formulaString.Split(formulaOperator);
            if (operandCandidates.Length != 2)
                throw new FormulaFormatException();

            Coordinates leftOperandCoordinates = coordinatesParser.Parse(operandCandidates[0], parsedHomesheetName);
            Coordinates rightOperandCoordinates = coordinatesParser.Parse(operandCandidates[1], parsedHomesheetName);
            if (leftOperandCoordinates == default(Coordinates) || rightOperandCoordinates == default(Coordinates))
                throw new FormulaFormatException();

            return new BinaryFormula(formulaOperator, leftOperandCoordinates, rightOperandCoordinates);
        }
    }
}
