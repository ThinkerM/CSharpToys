using System;
using System.Collections.Generic;
using ExpressionsCore.Expressions;

namespace ExpressionEvaluationService.Parsing
{
    public interface IExpressionParser
    {
        /// <summary>
        /// Parse an expression from a raw expression string. 
        /// May throw <see cref="FormatException"/> if the expression has issues related to operand/operator order or count, unrecognizable symbols, etc.
        /// </summary>
        /// <param name="expressionString"></param>
        /// <returns>Parsed expression tree</returns>
        /// <exception cref="FormatException">Invalid expression format</exception>
        IExpression Parse(string expressionString);

        /// <summary>
        /// Parse an expression from defined tokens.
        /// May throw <see cref="FormatException"/> if the expression has issues related to operand/operator order or count, unrecognizable symbols, etc.
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns>Parsed expression tree</returns>
        /// <exception cref="FormatException">Invalid expression format</exception>
        IExpression Parse(IEnumerable<string> tokens);

        /// <summary>
        /// Attempt to parse an expression from a raw expression string and indicate whether the parsing was successful.
        /// </summary>
        /// <param name="expressionString"></param>
        /// <param name="resultExpression"></param>
        /// <returns>True if building of the expression tree was successful, False if an error was encountered.
        /// <para>In case of success, the result expression tree is contained in the <see cref="resultExpression"/> parameter.</para></returns>
        bool TryParse(string expressionString, out IExpression resultExpression);

        /// <summary>
        /// Attempt to parse an expression from a raw expression string and indicate whether the parsing was successful.
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="resultExpression"></param>
        /// <returns>True if building of the expression tree was successful, False if an error was encountered.
        /// <para>In case of success, the result expression tree is contained in the <see cref="resultExpression"/> parameter.</para></returns>
        bool TryParse(IEnumerable<string> tokens, out IExpression resultExpression);
    }
}
