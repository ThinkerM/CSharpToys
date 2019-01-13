using System;
using System.Collections.Generic;
using System.Linq;
using TextAlignment.Contracts;
using TextAlignment.Enums;
using TextAlignment.FileManipulation;
using TextAlignment.Text.ReadingInfo;

namespace TextAlignment.Text
{
    internal class FormattingController
    {
        private ITextEvaluator Evaluator { get; }
        private ILineBuilder LineBuilder { get; }
        private IInputOutputWrapper IoWrapper { get; }
        private IReader Reader { get; }
        private int RequiredLineLength { get; }

        public FormattingController(IInputOutputWrapper ioWrapper, 
                                    ILineBuilder lineBuilder,
                                    ITextEvaluator evaluator, 
                                    int requiredLineLength)
        {
            IoWrapper = ioWrapper;
            LineBuilder = lineBuilder;
            Evaluator = evaluator;
            RequiredLineLength = requiredLineLength;
            Reader = new Reader(IoWrapper);
            Reader.NewParagraphEncountered += OnNewParagraphFound;
        }

        public void Justify()
            => Format(Padding.Justified);

        public void PadLeft()
            => Format(Padding.Left);

        //TODO Implement right-padding, see TextEvaluator + likely a right-based ILineBuilder (assigned to: Random slave [you know who I mean])
        public void PadRight()
            => throw new NotImplementedException();

        private readonly IList<string> wordsForCurrentLine = new List<string>();

        private void Format(Padding padding)
        {
            Reader.SkipWhitespaces(); //skip any initial non-printable characters
            if (IoWrapper.EndOfInput)
            {
                IoWrapper.WriteLine(LineBuilder.BuildEmptyLine());
                return;
            }

            foreach (var word in Reader.ReadWords())
            {
                if (Evaluator.CanAppendToLine(word, wordsForCurrentLine, RequiredLineLength))
                {
                    wordsForCurrentLine.Add(word);
                }
                else
                {
                    //line overflow => output last line with given padding before adding new word
                    OutputLine(wordsForCurrentLine, padding);
                    wordsForCurrentLine.Clear();
                    wordsForCurrentLine.Add(word);
                }
            }

            Cleanup(wordsForCurrentLine);
        }

        private void OnNewParagraphFound(object sender, EventArgs e)
        {
            OutputLine(wordsForCurrentLine, Padding.Left);
            IoWrapper.WriteLine(LineBuilder.BuildEmptyLine()); //empty line as paragraph separator

            wordsForCurrentLine.Clear();
        }

        private void Cleanup(IList<string> remainingWords)
        {
            if (remainingWords.Any())
            {
                OutputLine(remainingWords, Padding.Left);
            }
            IoWrapper.FlushOutput();
        }

        private void OutputLine(IList<string> words, Padding padding)
        {
            var insertions = Evaluator.InsertionSizes(words, RequiredLineLength, padding);
            string formattedLine = LineBuilder.BuildLine(words, insertions);
            IoWrapper.WriteLine(formattedLine);
        }
    }
}
