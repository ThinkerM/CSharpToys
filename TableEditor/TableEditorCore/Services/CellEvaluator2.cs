using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using TableEditorCore.Calculation;
using TableEditorCore.TableComponents.Cells;
using TableEditorCore.TableComponents.Cells.Content;

namespace TableEditorCore.Services
{
    internal class CellEvaluator2 : ICellEvaluator
    {
        private readonly ICellFinder cellFinder;
        private Stack<EvaluationNode> evaluationStack;

        public CellEvaluator2(ICellFinder cellFinder)
        {
            this.cellFinder = cellFinder;
        }

        public void Evaluate(ICell cell)
        {
            if (cell.Data.IsEvaluated)
                return;

            evaluationStack = new Stack<EvaluationNode>();
            evaluationStack.Push(new EvaluationNode(cell, null));

            Debug.WriteLine($"Evaluating [{cell.Coordinates}]");
            TraverseEvaluationTree();
            Cleanup();
        }

        private void TraverseEvaluationTree()
        {
            while (evaluationStack.Count > 0)
            {
                var currentNode = evaluationStack.Pop();
                ICell currentCell = currentNode.Cell;
                IBinaryFormula currentFormula = currentCell.Formula;
                Debug.Write($"  [{currentCell.Coordinates}] {currentCell.View}: ");
                Contract.Assume(currentFormula != null);
                Contract.Assume(!currentCell.Data.IsEvaluated);

                //cell will either end up being evaluated or pushed back onto stack, 
                //set to false here so that it doesn't have to be added before every continue/break control statement (cycle by self-reference is therefore checked explicitly)
                currentCell.BeingEvaluated = false;

                if (!currentFormula.OperandsValid(cellFinder))
                {
                    //some of the operands weren't found or don't have values which can be evaluated
                    currentCell.Data = new ErrorContent(currentCell.Data, ErrorType.DataError).Evaluated();
                    Debug.WriteLine("Invalid operands, error and continue");
                    continue;
                }

                var operands = new[] { currentFormula.LeftOperand, currentFormula.RightOperand };

                //cycle check
                if (operands.Any(x => x.BeingEvaluated) || operands.Any(x => x == currentCell))
                {
                    MarkCycle(currentNode);
                    Debug.WriteLine("Found cycle, mark and break");
                    break;
                }

                if (operands.All(x => x.Data.IsEvaluated))
                {
                    SafelyCalculateFormula(currentCell);
                    continue;
                }

                currentCell.BeingEvaluated = true;
                evaluationStack.Push(currentNode);

                evaluationStack.PushMultiple(
                    operands.Select(operand => new EvaluationNode(operand, currentNode)),
                    node => !node.Cell.Data.IsEvaluated);
            }
        }

        private void Cleanup()
        {
            foreach (EvaluationNode node in evaluationStack)
            {
                node.Cell.BeingEvaluated = false;
            }
        }

        private void SafelyCalculateFormula(ICell currentCell)
        {
            try
            {
                int formulaResult = currentCell.Formula.Calculate(cellFinder);
                currentCell.Data = new ValueContent(currentCell.Data, formulaResult).Evaluated();
            }
            catch (DivideByZeroException)
            {
                currentCell.Data = new ErrorContent(currentCell.Data, ErrorType.ZeroDivision).Evaluated();
            }
        }

        private static void MarkCycle(EvaluationNode cycledNode)
        {
            var currentNode = cycledNode;
            while (currentNode != null)
            {
                currentNode.Cell.Data = new ErrorContent(currentNode.Cell.Data, ErrorType.Cycle).Evaluated();
                currentNode = currentNode.PreviousNode;
            }
        }

        private class EvaluationNode
        {
            public EvaluationNode(ICell cell, EvaluationNode previousNode)
            {
                Cell = cell;
                cell.BeingEvaluated = true; //any cell with a formula in an evaluation stack is being evaluated
                PreviousNode = previousNode;
            }

            public ICell Cell { get; }

            /// <summary>
            /// Back-edge (cell whose formula references the <see cref="Cell"/>)
            /// </summary>
            public EvaluationNode PreviousNode { get; }
        }
    }
}
