using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TableEditorCore.Calculation;
using TableEditorCore.TableComponents.Cells;
using TableEditorCore.TableComponents.Cells.Content;

namespace TableEditorCore.Services
{
    internal class CellEvaluator : ICellEvaluator
    {
        private readonly ICellFinder cellFinder;
        private Stack<EvaluationNode> evaluationStack;

        public CellEvaluator(ICellFinder cellFinder)
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

                //cell will either end up being evaluated or pushed back onto stack, 
                //set to false here so that it doesn't have to be added before every continue/break control statement (cycle by self-reference is therefore checked explicitly)
                currentCell.BeingEvaluated = false;

                if (currentCell.Data.IsEvaluated)
                {
                    Debug.WriteLine("Already evaluated, continue");
                    continue; //evaluation already done, don't evaluate again
                }

                if (currentCell.Formula == null)
                {
                    //no formula, nothing to evaluate
                    currentCell.Data.Evaluated();
                    Debug.WriteLine("No formula, continue");
                    continue;
                }

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
                    Debug.WriteLine("Everything ok, calculate");
                    continue;
                }

                InsertNodeAndOperands(currentNode);
            }
        }

        private void InsertNodeAndOperands(EvaluationNode node)
        {
            //not all operands evaluated yet, push self back onto the stack, then push both operands
            node.Cell.BeingEvaluated = true; //set marker due to being pushed back onto the evaluation stack
            evaluationStack.Push(node);

            var formula = node.Cell.Formula;
            evaluationStack.Push(new EvaluationNode(cell: formula.RightOperand, previousNode: node));
            evaluationStack.Push(new EvaluationNode(cell: formula.LeftOperand, previousNode: node));
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
