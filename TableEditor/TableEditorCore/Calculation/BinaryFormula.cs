using System;
using TableEditorCore.Services;
using TableEditorCore.TableComponents;
using TableEditorCore.TableComponents.Cells;

namespace TableEditorCore.Calculation
{
    public interface IBinaryFormula
    {
        int Calculate(ICellFinder cellFinder);
        bool OperandsValid(ICellFinder cellFinder);
        ICell LeftOperand { get; }
        ICell RightOperand { get; }
    }

    public class BinaryFormula : IBinaryFormula
    {
        private readonly Func<int,int,int> operation;

        private Coordinates LeftOperandCoordinates { get; }
        private Coordinates RightOperandCoordinates { get; }
        public ICell LeftOperand { get; private set; }
        public ICell RightOperand { get; private set; }

        public BinaryFormula(char operatorSymbol, Coordinates leftOperandCoordinates, Coordinates rightOperandCoordinates)
        {
            LeftOperandCoordinates = leftOperandCoordinates;
            RightOperandCoordinates = rightOperandCoordinates;
            switch (operatorSymbol)
            {
                case '+':
                    operation = (x, y) => x + y; 
                    break;
                case '-':
                    operation = (x, y) => x - y;
                    break;
                case '*':
                    operation = (x, y) => x * y;
                    break;
                case '/':
                    operation = (x, y) => x / y;
                    break;
                default:
                    throw new ArgumentException("Could not convert to a known binary operator.", nameof(operatorSymbol));
            }
        }

        public int Calculate(ICellFinder cellFinder)
        {
            if (!OperandsValid(cellFinder))
            {
                throw new InvalidOperationException("Trying to invoke calculation when not all operands are valid.");
            }
            return operation(LeftOperand.Data.Value, RightOperand.Data.Value);
        }

        private bool? cachedValidity;
        public bool OperandsValid(ICellFinder cellFinder)
        {
            if (!cachedValidity.HasValue)
            {
                LeftOperand = LeftOperand ?? (LeftOperand = cellFinder.Find(LeftOperandCoordinates));
                RightOperand = RightOperand ?? (RightOperand= cellFinder.Find(RightOperandCoordinates));
                cachedValidity = (LeftOperand != null
                                  && RightOperand != null
                                  && LeftOperand.Data.Valid
                                  && RightOperand.Data.Valid);
            }
            return cachedValidity.Value;
        }
    }
}
