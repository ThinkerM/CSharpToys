using TableEditorCore.Calculation;

namespace TableEditorCore.Parsing.Interfaces
{
    internal interface IBinaryFormulaParser
    {
        IBinaryFormula Parse(string formulaString, string parsedHomesheetName);
    }
}