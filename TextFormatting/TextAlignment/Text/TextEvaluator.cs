using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TextAlignment.Contracts;
using TextAlignment.Extensions;
using TextAlignment.Enums;

namespace TextAlignment.Text
{
    internal class TextEvaluator : ITextEvaluator
    {
        #region Insertion Sizes
        public IEnumerable<int> InsertionSizes(ICollection<string> wordsToInclude, int requiredLineLength, Padding padding)
        {
            return InsertionSizes(
                wordsToInclude.Sum(x => x.Length),
                wordsToInclude.Count,
                requiredLineLength,
                padding);
        }

        public IEnumerable<int> InsertionSizes(int wordsToIncludeLength, int wordsToIncludeCount, int requiredLineLength, Padding padding)
        {
            //TODO: Implement right-padded insertion sizes (assigned to: Random slave)
            if (padding == Padding.Right)
                throw new NotImplementedException();

            if (padding == Padding.Left 
                || wordsToIncludeCount <= 1 
                || requiredLineLength < MinimumLineLengthWithSpaces(wordsToIncludeLength, wordsToIncludeCount))
            {
                return InsertionSizesLeft(wordsToIncludeCount);
            }

            return InsertionSizesJustified(wordsToIncludeLength,
                                           wordsToIncludeCount,
                                           requiredLineLength);
        }

        private static IEnumerable<int> InsertionSizesLeft(int wordsToIncludeCount)
        {
            int separations = wordsToIncludeCount - 1;
            for (int i = 0; i < separations; i++)
            {
                yield return 1;
            }
        }

        private static IEnumerable<int> InsertionSizesJustified(int wordsToIncludeLength,
                                                             int wordsToIncludeCount,
                                                             int requiredLineLength)
        {
            int totalSpacesToFill = RequiredInsertions(wordsToIncludeLength, requiredLineLength);
            int totalSeparations = wordsToIncludeCount - 1;

            int baseSpaceAmount = totalSpacesToFill / totalSeparations;
            int extraSpaceAmount = baseSpaceAmount + 1;

            int separationsWithExtraSpace = totalSpacesToFill % totalSeparations;
            int separationswithBaseSpace = totalSeparations - separationsWithExtraSpace;

            Debug.Assert(separationswithBaseSpace + separationsWithExtraSpace == totalSeparations);
            Debug.Assert(separationsWithExtraSpace * extraSpaceAmount + separationswithBaseSpace * baseSpaceAmount == totalSpacesToFill);

            //return extra-spaced separations first
            for (int i = 0; i < separationsWithExtraSpace; i++)
            {
                yield return extraSpaceAmount;
            }

            //return remaining normal-spaced separations
            for (int i = 0; i < separationswithBaseSpace; i++)
            {
                yield return baseSpaceAmount;
            }
        }
        #endregion

        #region Appending
        public bool CanAppendToLine(string word, ICollection<string> wordsOnLine, int requiredLineLength)
        {
            if (wordsOnLine.Count == 0) //if line is otherwise empty, word has to be appended regardless
                return true;

            int wordsOnLineLength = wordsOnLine.Sum(x => x.Length);
            int minimalCurrentLineLength = MinimumLineLengthWithSpaces(wordsOnLineLength, wordsOnLine.Count);

            return RequiredLineLengthWithWordAdded(word.Length, minimalCurrentLineLength) <= requiredLineLength;
        }

        public bool CanAppendToWord(char value)
            => value.IsPrintable();
        #endregion

        #region Helper methods
        private static int RequiredInsertions(int wordLengthSum, int desiredLength)
            => desiredLength - wordLengthSum;

        private static int RequiredLineLengthWithWordAdded(int wordLength, int lineLength)
            => lineLength > 0 
                ? wordLength + lineLength + 1 //word needs an extra space to be separated from rest of line
                : wordLength; 

        private static int MinimumLineLengthWithSpaces(int wordLengthSum, int wordCount)
            => wordLengthSum + wordCount - 1;
        #endregion
    }
}
