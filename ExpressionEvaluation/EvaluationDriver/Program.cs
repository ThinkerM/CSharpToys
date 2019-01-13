//#define HINTS
using System;
using System.Collections.Generic;
using System.IO;
using CommonLibrary;
using ExpressionEvaluationService.Evaluation;
using ExpressionEvaluationService.Parsing;

namespace EvaluationDriver
{
    internal static class Program
    {
        private static readonly Dictionary<string, EvaluationStrategy> PossibleActions = new Dictionary<string, EvaluationStrategy>
        {
            { "i", EvaluationStrategy.Integer },
            { "d", EvaluationStrategy.Real },
            { "p", EvaluationStrategy.InfixFullParentheses },
            { "P", EvaluationStrategy.InfixMinimalParentheses }
        };

        private static void Main()
        {
            TextWriter output = Console.Out;
            output.NewLine = "\n";

            var evaluator = new EvaluationEngine();
            string currentLine;
            while ((currentLine = Console.ReadLine()) != null && currentLine != "end")
            {
                ProcessLine(currentLine, evaluator, output);
            }
        }

        private static void ProcessLine(string currentLine, EvaluationEngine engine, TextWriter output)
        {
            if (!currentLine.IsNullOrEmpty())
            {
                EvaluationStrategy evaluationStrategy;
                if (PossibleActions.TryGetValue(currentLine, out evaluationStrategy))
                {
                    engine.EvaluateLastExpression(evaluationStrategy, output);
                }
                else
                {
                    string expressionString = currentLine.TrimStart('=');
                    engine.InsertExpression(expressionString, ExpressionNotationType.Prefix, output);
                }
            }
#if HINTS
            else //make an empty line to get hints
            {
                OutputHint();
            }
        }

        private static void OutputHint()
        {
            var defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (var action in PossibleActions)
            {
                Console.WriteLine($"[{action.Key}]: {action.Value}");
            }
            Console.ForegroundColor = defaultColor;
#endif
        }
    }
}
