using System.IO;

namespace ReflectorSerializer
{
    public interface ISerializer
    {
        int IndentSpaceCount { set; }
        bool TreatStringAsEnumerable { set; }
        void Serialize(TextWriter output, object entity);
    }
}
