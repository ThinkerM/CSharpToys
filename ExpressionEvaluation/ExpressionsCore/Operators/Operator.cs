using System.Collections.Generic;
using System.Linq;

namespace ExpressionsCore.Operators
{

    public abstract class Operator
    {
        protected Operator(char mappedSymbol, int precedence)
        {
            MappedSymbol = mappedSymbol;
            Precedence = precedence;
        }

        public char MappedSymbol { get; }
        public int Precedence { get; }
        public abstract int RequiredOperandsCount { get; }

        #region Static operator mapping

        public static readonly Operator Add = new BinaryOperator('+', 1);
        public static readonly Operator Subtract = new BinaryOperator('-', 1);
        public static readonly Operator Multiply = new BinaryOperator('*', 2);
        public static readonly Operator Divide = new BinaryOperator('/', 2);
        public static readonly Operator Negate = new UnaryOperator('~', 3);

        private static readonly HashSet<Operator> DefinedOperators = new HashSet<Operator>
        {
            Add,
            Subtract,
            Multiply,
            Divide,
            Negate
        };

        public static bool IsDefined(char operatorSymbol)
            => DefinedOperators.Any(o => o.MappedSymbol == operatorSymbol);

        public static Operator GetMapped(char operatorSymbol)
            => DefinedOperators.SingleOrDefault(o => o.MappedSymbol == operatorSymbol);

        #endregion

        protected class UnaryOperator : Operator
        {
            public UnaryOperator(char mappedSymbol, int precedence) : base(mappedSymbol, precedence) { }
            public override int RequiredOperandsCount => 1;
        }

        protected class BinaryOperator : Operator
        {
            public BinaryOperator(char mappedSymbol, int precedence) : base(mappedSymbol, precedence) { }
            public override int RequiredOperandsCount => 2;
        }
    }
}
