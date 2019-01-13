using System.IO;

namespace NezarkaController.CommandsLayer
{
    /// <summary>
    /// Representation of a command/adress which can result in a page being generated
    /// </summary>
    internal interface ICommand
    {
        void GenerateRelatedView(TextWriter outputTarget);
    }
}
