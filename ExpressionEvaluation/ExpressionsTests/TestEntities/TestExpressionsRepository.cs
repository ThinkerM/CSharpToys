using System.Collections.Generic;
using ExpressionsCore.Expressions;
using ExpressionsCore.Expressions.Implementations;

namespace ExpressionsTests.TestEntities
{
    internal static class TestExpressionsRepository
    {
        public static readonly IEnumerable<TestExpressionWrapper> TestExpressions = new List<TestExpressionWrapper>
        {
            new TestExpressionWrapper(
                new Negation(
                    new ConstantExpression(10)), 
                "(-10)", "-10", -10),

            new TestExpressionWrapper(
                new Subtraction(
                    new Negation(
                        new ConstantExpression(1)),
                    new ConstantExpression(3)),
                "((-1)-3)", "-1-3", -4),

            new TestExpressionWrapper(
                new Division(
                    new Addition(
                        new ConstantExpression(3),
                        new ConstantExpression(9)), 
                    new Multiplication(
                        new ConstantExpression(3), 
                        new Negation(
                            new ConstantExpression(2)))),
                "((3+9)/(3*(-2)))", "(3+9)/(3*-2)", -2),

            new TestExpressionWrapper(
                new Division(
                    new Multiplication(
                        new ConstantExpression(2),
                        new ConstantExpression(6)),
                    new Division(
                        new ConstantExpression(3),
                        new ConstantExpression(2))),
                "((2*6)/(3/2))", "2*6/(3/2)", 12),

            new TestExpressionWrapper(
                new Subtraction(
                    new Addition(
                        new ConstantExpression(3),
                        new ConstantExpression(2)), 
                    new Addition(
                        new ConstantExpression(3),
                        new ConstantExpression(1))),
                "((3+2)-(3+1))", "3+2-(3+1)", 1),

            new TestExpressionWrapper(
                new Negation(
                    new Multiplication(
                        new Multiplication(
                            new Multiplication(
                                new ConstantExpression(1),
                                new ConstantExpression(1)),
                            new ConstantExpression(2)),
                        new ConstantExpression(3))),
                "(-(((1*1)*2)*3))", "-(1*1*2*3)", -6),

            new TestExpressionWrapper(
                new Negation(
                    new Multiplication(
                        new Multiplication(
                            new Multiplication(
                                new ConstantExpression(1),
                                new ConstantExpression(1)),
                            new ConstantExpression(2)),
                        new Negation(
                            new ConstantExpression(3)))),
                "(-(((1*1)*2)*(-3)))", "-(1*1*2*-3)", 6),

            new TestExpressionWrapper(
                new Multiplication(
                    new Division(
                        new ConstantExpression(3), 
                        new Negation(
                            new ConstantExpression(1))), 
                    new Subtraction(
                        new ConstantExpression(0), 
                        new Negation(
                            new ConstantExpression(2)))), 
                "((3/(-1))*(0-(-2)))", "3/-1*(0--2)", -6),

            new TestExpressionWrapper(
                new Addition(
                    new Division(
                        new ConstantExpression(3), 
                        new ConstantExpression(2)), 
                    new Subtraction(
                        new Multiplication(
                            new ConstantExpression(10),
                            new Negation(
                                new ConstantExpression(1))), 
                        new ConstantExpression(9))),
                "((3/2)+((10*(-1))-9))", "3/2+10*-1-9", -18),

            new TestExpressionWrapper(
                new Addition(
                    new Negation(
                        new Multiplication(
                            new ConstantExpression(3),
                            new ConstantExpression(1))), 
                    new ConstantExpression(10)),
                "((-(3*1))+10)", "-(3*1)+10", 7)
        };
    }
}
