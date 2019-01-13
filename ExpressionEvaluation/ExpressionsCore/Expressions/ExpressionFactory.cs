using System;
using System.Collections.Generic;
using System.Linq;
using ExpressionsCore.Expressions.Implementations;
using ExpressionsCore.Operators;

namespace ExpressionsCore.Expressions
{
    public interface IExpressionFactory
    {
        /// <summary>
        /// Create an <see cref="ArgumentException"/> based on the given operator and specified operand expressions.
        /// <para>If the amount of operands doesn't match the <see cref="Operator"/>'s arity, an <see cref="ArgumentException"/> is thrown.</para>
        /// </summary>
        /// <param name="op"></param>
        /// <param name="operands"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        IExpression CreateExpression(Operator op, IEnumerable<IExpression> operands);

        /// <summary>
        /// Create an <see cref="IExpression"/> with a constant value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        IExpression CreateExpression(int value);
    }

    public class ExpressionFactory : IExpressionFactory
    {
        public IExpression CreateExpression(Operator op, IEnumerable<IExpression> operands)
        {
            var operandsArray = operands.ToArray();
            if (op.RequiredOperandsCount != operandsArray.Length)
            {
                throw new ArgumentException($"The number of provided {nameof(operands)} didn't match the operator's arity "
                                            + $"(operands count {operandsArray.Length}, operator requires {op.RequiredOperandsCount}", nameof(operands));
            }
            switch (op.RequiredOperandsCount)
            {
                case 1:
                    return UnaryExpressionFactory.CreateOperation(op, operandsArray[0]);
                case 2:
                    return BinaryExpressionFactory.CreateOperation(op, operandsArray[0], operandsArray[1]);
                default:
                    throw new ArgumentException($"Invalid amount of operands: {operandsArray.Length}. No operations defined for this amount.");
            }
        }

        public IExpression CreateExpression(int value)
        {
            return new ConstantExpression(value);
        }

        private static class BinaryExpressionFactory
        {
            private static readonly Dictionary<Operator, Func<IExpression, IExpression, IExpression>> MappedOperations = new Dictionary<Operator, Func<IExpression, IExpression, IExpression>>
            {
                { Operator.Add, (left, right) => new Addition(left, right) },
                { Operator.Subtract, (left, right) => new Subtraction(left, right) },
                { Operator.Multiply, (left, right) => new Multiplication(left, right) },
                { Operator.Divide, (dividend, divisor) => new Division(dividend, divisor) }
            };

            public static IExpression CreateOperation(Operator op, IExpression leftOperand, IExpression rightOperand)
            {
                if (!IsDefined(op))
                    throw new ArgumentException($"No operation defined for operator: {op.MappedSymbol}");

                return MappedOperations[op](leftOperand, rightOperand);
            }

            private static bool IsDefined(Operator op)
                => MappedOperations.ContainsKey(op);
        }

        private static class UnaryExpressionFactory
        {
            private static readonly Dictionary<Operator, Func<IExpression, IExpression>> MappedOperations = new Dictionary<Operator, Func<IExpression, IExpression>>
            {
                { Operator.Negate, (operand) => new Negation(operand) }
            };

            public static IExpression CreateOperation(Operator op, IExpression operand)
            {
                if (!IsDefined(op))
                    throw new ArgumentException($"No operation defined for operator: {op.MappedSymbol}");

                return MappedOperations[op](operand);
            }

            private static bool IsDefined(Operator operatorSymbol)
                => MappedOperations.ContainsKey(operatorSymbol);
        }
    }
}
