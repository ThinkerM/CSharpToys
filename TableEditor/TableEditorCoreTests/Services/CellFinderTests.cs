using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using TableEditorCore.Calculation;
using TableEditorCore.Parsing.Interfaces;
using TableEditorCore.Services;
using TableEditorCore.TableComponents;
using TableEditorCore.TableComponents.Cells;

namespace TableEditorCoreTests.Services
{
    [TestFixture()]
    public class CellFinderTests
    {
        private ICellFinder cellFinder;

        [SetUp]
        public void AddMainSheetCells()
        {
            cellFinder = new CellFinder(new Sheet("test mainSheet"), new MockSheetParser());
            cellFinder.MainSheet.AddCell(new FormulaCell(CreateMainSheetCoordinates(), new MockFormula()));
        }

        [SetUp]
        public void PrepareSheetFiles()
        {
            File.Create("testSheet1.sheet").Close();
            File.Create("testSheet2.sheet").Close();
            File.Create("badSheet.txt").Close(); //improper extension
        }

        [TearDown]
        public void RemoveSheetFiles()
        {
            File.Delete("testSheet1.sheet");
            File.Delete("testSheet2.sheet");
            File.Delete("badSheet.txt");
        }

        [Test]
        public void FindInMainSheetTest()
        {
            Assert.IsNotNull(cellFinder.Find(CreateMainSheetCoordinates()));
        }

        [Test]
        public void FindNonexistentInMainSheetTest()
        {
            Assert.IsNull(cellFinder.Find(new Coordinates("ASDFG", 123, "")));
        }

        [Test]
        public void FindInNewSheetTest()
        {
            Assert.IsNotNull(cellFinder.Find(CreateSheetCoordinates("testSheet1")));
            Assert.IsNotNull(cellFinder.Find(CreateSheetCoordinates("testSheet1")));

            Assert.IsNotNull(cellFinder.Find(CreateSheetCoordinates("testSheet1")));
            Assert.IsNotNull(cellFinder.Find(CreateSheetCoordinates("testSheet1")));


        }

        private static Coordinates CreateMainSheetCoordinates()
            => new Coordinates("A", 1, "");

        private static Coordinates CreateSheetCoordinates(string sheetName)
            => new Coordinates("A", 1, sheetName);

        private class MockSheetParser : ISheetParser
        {
            public ISheet Parse(TextReader sheetFile, string sheetName = null)
            {
                var mockSheet = new Sheet("");
                mockSheet.AddCell(new FormulaCell(new Coordinates("A", 1, sheetName), new MockFormula()));
                return mockSheet;
            }

            public ICell ParseAtCoordinates(TextReader sheetFileReader, Coordinates coordinates)
            {
                return new FormulaCell(coordinates, new MockFormula());
            }
        }

        private class MockFormula : IBinaryFormula
        {
            public Coordinates LeftOperandCoordinates { get; } = default(Coordinates);
            public Coordinates RightOperandCoordinates { get; } = default(Coordinates);
            public int Calculate(ICellFinder cellFinder1) => 0;
            public bool OperandsValid(ICellFinder cellFinder) => true;
            public ICell LeftOperand { get; } = null;
            public ICell RightOperand { get; } = null;
        }
    }
}