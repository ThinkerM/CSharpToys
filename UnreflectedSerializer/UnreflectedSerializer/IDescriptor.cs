using System;
using System.IO;

namespace UnreflectedSerializer
{
    public interface IDescriptor<TEntity>
    {
        void Serialize(TEntity instance, TextWriter writer, int tabsOffset = 0);
        TEntity Deserialize(TextReader reader);
        IDisposable AdaptToProperty(string value);
    }
}
