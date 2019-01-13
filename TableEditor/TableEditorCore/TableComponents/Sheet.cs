using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using TableEditorCore.Services;
using TableEditorCore.TableComponents.Cells;

namespace TableEditorCore.TableComponents
{
    internal class Sheet : ISheet
    {
        private readonly List<List<ICell>> table = new List<List<ICell>>();

        public string Name { get; }

        public Sheet(string name)
        {
            Name = name;
        }

        public void AddCell(ICell cell)
        {
            var coordinates = cell.Coordinates;
            FillEmptyRows(coordinates.Row);
            table[coordinates.Row - 1].Add(cell);
            Debug.Assert(table[coordinates.Row - 1].Count == coordinates.Column.AsOrdinalNumber());
        }

        private void FillEmptyRows(int targetCount)
        {
            for (int i = table.Count; i < targetCount; i++)
            {
                table.Add(new List<ICell>());
            }
        }

        public ICell this[Coordinates coordinates]
        {
            get
            {
                int columnNumber = (int)coordinates.Column.AsOrdinalNumber() - 1; //if it's such a high column, just frickin' trim it :D
                if (coordinates.Row >= table.Count
                    || columnNumber >= table[coordinates.Row - 1].Count)
                {
                    return CellGenerator.CreateEmptyCell(coordinates);
                }
                return table[coordinates.Row - 1][columnNumber];
            }
        }

        private IEnumerable<List<ICell>> GetRows()
        {
            return table;
        }

        public void DescribeSelf(TextWriter outputTarget)
        {
            foreach (var row in table)
            {
                outputTarget.Write(row.First().View);
                foreach (var cell in row.Skip(1))
                {
                    outputTarget.Write(" " + cell.View);
                }
                outputTarget.WriteLine();
            }
        }

        public void CalculateContentData(ICellEvaluator evaluator)
        {
            foreach (var row in table)
            {
                foreach (ICell cell in row)
                {
                    evaluator.Evaluate(cell);
                }
            }
        }
    }
}
