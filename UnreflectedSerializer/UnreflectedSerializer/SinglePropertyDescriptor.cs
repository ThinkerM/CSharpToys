using System;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace UnreflectedSerializer
{
    public class SinglePropertyDescriptor<TEntity, TValue> : IMemberDescriptor<TEntity>
    {
        public SinglePropertyDescriptor(Expression<Func<TEntity, TValue>> property, string propertyName = null)
        {
            propertyAccess = property.Compile();
            propertyAssignment = property.ToSetter();
            PropertyName = propertyName ?? property.GetName();

            //lambda compilation and especially the setter thingy can slow it a down somewhat, but declaring it something like
            // ...<Person, string>(person => person.FirstName, (person, name) => person.FirstName = name);
            //with the user having to send in both the getter and the setter would be a major pain imho.
        }

        public string PropertyName { get; }
        private readonly Func<TEntity, TValue> propertyAccess;
        private readonly Action<TEntity, TValue> propertyAssignment;

        public void PopulateEntity(TEntity targetEntity, TextReader reader)
        {
            string line = reader.ReadLine()?.Trim();
            if (line == PropertyName.ToXmlOpeningTag()) //skip over opening tag if still present
                line = reader.ReadLine();

            if (!line.TryConvert(out TValue convertedValue))
            {
                throw new SerializationException(
                    $"Could not deserialize value '{line}' into property '{PropertyName}' of type {typeof(TValue)}.");
            }
            propertyAssignment(targetEntity, convertedValue);

            if (reader.ReadLine()?.Trim() != PropertyName.ToXmlClosingTag()) //dispose of closing tag line and ensure format correctness
                throw new SerializationException(
                    $"Closing tag for property '{PropertyName}' missing. Expected '{PropertyName.ToXmlClosingTag()}'.");
        }

        public void SerializeMember(TEntity sourceEntity, TextWriter writer, int tabsOffset = 0)
        {
            if (sourceEntity == null) return;
            TValue propertyValue = propertyAccess(sourceEntity);
            if (propertyValue == null) return;

            writer.WriteLineOffset(PropertyName.ToXmlOpeningTag(), tabsOffset);
            writer.WriteLineOffset(propertyValue.ToString(), tabsOffset + 1);
            writer.WriteLineOffset(PropertyName.ToXmlClosingTag(), tabsOffset);
        }
    }
}