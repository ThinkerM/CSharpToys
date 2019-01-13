using System.ComponentModel;
using System.Diagnostics;

namespace TableEditorCore.Calculation
{
    public enum ErrorType
    {
        /// <summary>
        /// No error encountered
        /// </summary>
        None,

        /// <summary>
        /// Calculation cannot be performed due to missing data or any other general problem with involved operands
        /// </summary>
        DataError,

        /// <summary>
        /// Involved operands refer to one another in a cycle
        /// </summary>
        Cycle,

        /// <summary>
        /// Calculation resulted in a zero division
        /// </summary>
        ZeroDivision
    }

    public static class ErrorTypeExtensions
    {
        [DebuggerStepThrough]
        public static string ToStringRepresentation(this ErrorType errorType)
        {
            switch (errorType)
            {
                case ErrorType.None:
                    return "";
                case ErrorType.DataError:
                    return CalculationConstants.CALCULATION_ERROR;
                case ErrorType.Cycle:
                    return CalculationConstants.CYCLE;
                case ErrorType.ZeroDivision:
                    return CalculationConstants.ZERO_DIVISION;
                default:
                    throw new InvalidEnumArgumentException($"{nameof(errorType)} with the value {errorType} not recognised, make sure all enum cases are covered.");
            }
        }
    }
}
