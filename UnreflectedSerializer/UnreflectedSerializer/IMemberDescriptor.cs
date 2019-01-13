using System.IO;

namespace UnreflectedSerializer
{
    public interface IMemberDescriptor<in TParentEntity>
    {
        string PropertyName { get; }
        void PopulateEntity(TParentEntity targetEntity, TextReader reader);
        void SerializeMember(TParentEntity sourceEntity, TextWriter writer, int tabsOffset = 0);
    }
}