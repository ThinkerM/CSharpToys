using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAlignment.Contracts;
using TextAlignment.Enums;

namespace TextAlignment.Text.LineBuilding
{
    internal class LineBuilderFactory : ILineBuilderFactory
    {
        public ILineBuilder CreateLineBuilder(LineBuilderType type)
        {
            switch (type)
            {
                case LineBuilderType.Basic:
                    return new SimpleLineBuilder();
                case LineBuilderType.WhitespaceHighlighter:
                    return new HighlightingLineBuilder(spaceHighlight: '.', endOfLineHighlight: "<-");
                default:
                    return new SimpleLineBuilder();
            }
        }
    }
}
