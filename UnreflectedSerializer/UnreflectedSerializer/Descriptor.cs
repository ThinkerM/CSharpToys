using System;
using System.IO;
using System.Runtime.Serialization;
using UnreflectedSerializer.Mapping;

namespace UnreflectedSerializer
{
    public class Descriptor<TEntity> : FluentMapper<TEntity, Descriptor<TEntity>>, //TDerived of FluentMapper is Descriptor<Entity> itself, so that chaining preserves Descriptor instead of Mapper
                                           IDescriptor<TEntity>
        where TEntity : new()
    {
        private string EntityName { get; set; } = typeof(TEntity).Name;

        public void Serialize(TEntity instance, TextWriter writer, int tabsOffset = 0)
        {
            if (instance == null) return;

            writer.WriteLineOffset(EntityName.ToXmlOpeningTag(), tabsOffset);
            foreach (var memberDescription in this.MappedProperties.Values)
            {
                memberDescription.SerializeMember(instance, writer, tabsOffset + 1);
            }
            writer.WriteLineOffset(EntityName.ToXmlClosingTag(), tabsOffset);
        }

        public TEntity Deserialize(TextReader reader)
        {
            var resultEntity = new TEntity();
            string xmlTag;
            while ((xmlTag = reader.ReadLine()?.Trim()) != EntityName.ToXmlClosingTag() && xmlTag != null) //avoid getting caught up if input is empty
            {
                string propertyName = xmlTag.RemoveXmlBrackets();
                if (propertyName == EntityName) continue; //skip opening tag

                if (!this.MappedProperties.TryGetValue(propertyName, out IMemberDescriptor<TEntity> propertyDescriptor))
                {
                    throw new SerializationException($"Unmapped property in xml: '{propertyName}'.");
                }
                propertyDescriptor.PopulateEntity(resultEntity, reader);
            }
            return resultEntity;
        }

        /// <summary>
        /// Allow for a temporary change of the property tag the <see cref="Descriptor{TEntity}"/> is supposed to look for during (de)serialization.
        /// This allows the descriptor to be used on different properties (with different tags) with the same mapping.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IDisposable AdaptToProperty(string value)
        {
            //possibly a slightly weird solution, feels better than exposing the EntityName publicly or constantly passing property names to (de)serialization though
            //the revert back to the original value isn't even necessary
            return new TemporaryPropertyTagChange(this, value);
        }

        private class TemporaryPropertyTagChange : IDisposable
        {
            private readonly string oldPropertyValue;
            private readonly Descriptor<TEntity> descriptor;
            public TemporaryPropertyTagChange(Descriptor<TEntity> descriptor, string newEntityValue)
            {
                this.descriptor = descriptor;
                oldPropertyValue = descriptor.EntityName;
                descriptor.EntityName = newEntityValue;
            }
            void IDisposable.Dispose()
            {
                descriptor.EntityName = oldPropertyValue;
            }
        }
    }
}
