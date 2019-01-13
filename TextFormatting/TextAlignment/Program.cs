using System.Collections.Generic;
using TextAlignment.Contracts;
using TextAlignment.Enums;
using TextAlignment.FileManipulation;
using TextAlignment.Text;
using TextAlignment.Text.LineBuilding;

namespace TextAlignment
{
    internal static partial class Program
    {
        /// <summary>
        /// <param name="args">Takes at least 3/4 arguments:
        /// <list type="number">
        ///    <para/><item>Optional: first argument can be "--highlight-spaces" (<see cref="string"/>, enforces at least 4 arguments)</item>
        ///     <para/><item>1st/2nd - N-2nd arguments point to input files paths (<see cref="string"/>)</item>
        ///    <para/><item>N-1st argument points to a single output file path (<see cref="string"/>)</item>
        ///    <para/><item>Last argument indicates required line length (<see cref="int"/>)</item>
        /// </list></param>
        /// </summary>
        private static void Main(string[] args)
        {
            if (!ValidArguments(args))
                return;

            int requiredLineLength;
            int requiredLineLengthIndex = args.Length - 1;
            int.TryParse(args[requiredLineLengthIndex], out requiredLineLength);

            bool highlightSpaces = args[0] == "--highlight-spaces";
            ILineBuilder lineBuilder = InitializeLineBuilder(highlightSpaces);

            using (IInputOutputWrapper io = InitializeIO(args, highlightSpaces))
            {
                var formatter = new FormattingController(io, lineBuilder, new TextEvaluator(), requiredLineLength);
                formatter.Justify(); 
            }
        }

        private static ILineBuilder InitializeLineBuilder(bool highlightSpaces)
        {
            LineBuilderType lbType = highlightSpaces
                ? LineBuilderType.WhitespaceHighlighter
                : LineBuilderType.Basic;
            ILineBuilderFactory lbFactory = new LineBuilderFactory();
            ILineBuilder lineBuilder = lbFactory.CreateLineBuilder(lbType);
            return lineBuilder;
        }

        private static IInputOutputWrapper InitializeIO(string[] args, bool highlightSpacesArgumentUsed)
        {
            int filePathsStartIndex = highlightSpacesArgumentUsed ? 1 : 0;
            int inputFilePathsCount = args.Length - 2 - filePathsStartIndex; //indices end at N-2nd argument (last is line width, pre-last is output file)
            IList<string> inputFilePaths = new List<string>(args).GetRange(filePathsStartIndex, inputFilePathsCount);

            int outputFilePathIndex = args.Length - 2;
            var output = FileLoader.ForceOpenForWriting(args[outputFilePathIndex]);

            return new MultiInputStreamWrapper(inputFilePaths, output);
        }
    }
}
