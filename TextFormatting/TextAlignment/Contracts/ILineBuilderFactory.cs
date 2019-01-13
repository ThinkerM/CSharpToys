using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAlignment.Enums;

namespace TextAlignment.Contracts
{
    internal interface ILineBuilderFactory
    {
        ILineBuilder CreateLineBuilder(LineBuilderType type);
    }
}
