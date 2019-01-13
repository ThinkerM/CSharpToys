using NUnit.Framework;
using TableEditorCore.Calculation;
using TableEditorCore.Parsing;
using TableEditorCore.Services;
using TableEditorCore.TableComponents;
using TableEditorCore.TableComponents.Cells;
using TableEditorCore.TableComponents.Cells.Content;

namespace TableEditorCoreTests.Parsing
{
    [TestFixture]
    public class BinaryFormulaParserTests
    {
        private readonly BinaryFormulaParser parser = new BinaryFormulaParser(new CoordinatesParser());

        [Test]
        [TestCase("=A1+A1", 6)]
        [TestCase("=A1-A1", 0)]
        [TestCase("=A1*A1", 9)]
        [TestCase("=A1/A1", 1)]
        [TestCase("=Sheet2!A1+A1", 6)]
        [TestCase("=Sheet2!A1-Sheet1!A1", 0)]
        [TestCase("=A1*Sheet1!A1", 10)]
        public void ParseValidFormulaTest(string formulaString, int expectedFormulaResult)
        {
            IBinaryFormula formula = parser.Parse(formulaString, "");
            int formulaResult = formula.Calculate(new MockCellFiner());
            Assert.AreEqual(expectedFormulaResult, formulaResult);
        }

        [Test]
        [TestCase("=A1L2")]
        [TestCase("=A1 L2")]
        [TestCase("=A1.L2")]
        [TestCase("=A1!L2")]
        public void ParseMissingOperatorFormulaTest(string missingOperatorFormula)
        {
            Assert.Throws<MissingOperatorException>(
                () => parser.Parse(missingOperatorFormula, ""));
        }

        [Test]
        [TestCase("=A1*L2-B1")]
        [TestCase("=A1*-B1")]
        [TestCase("=A1-B1-A1")]
        public void TooManyOperatorsTest(string tooManyOperatorsFormula)
        {
            Assert.Throws<FormulaFormatException>(
                () => parser.Parse(tooManyOperatorsFormula, ""));
        }

        [Test]
        [TestCase("A1+A2")]
        [TestCase("=A1+A 2")]
        [TestCase("=A1 + A2")]
        [TestCase("=(A1+A2)")]
        [TestCase("=Sheet8A1+A2")]
        [TestCase("=Sheet8! A1+A2")]
        [TestCase("=a1+B2")]
        [TestCase("=Sheet2!+B3")]
        public void InvalidFormatTest(string invalidFormatFormula)
        {
            Assert.Throws<FormulaFormatException>(
                () => parser.Parse(invalidFormatFormula, ""));
        }

        private class MockCellFiner : ICellFinder
        {
            public ISheet MainSheet { get; } = null;
            public ICell Find(Coordinates coordinates)
            {
                return new MockCell();
            }
        }

        private class MockCell : ICell
        {
            public Coordinates Coordinates { get; } = default(Coordinates);
            public ICellContent Data { get; set; } = new CellContent(3);
            public string View { get; } = null;
            public bool BeingEvaluated { get; set; } = false;
            public IBinaryFormula Formula { get; } = null;
        }
    }
}