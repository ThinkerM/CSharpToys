using System;
using System.IO;
using System.Linq.Expressions;
using UnreflectedSerializer.Mapping;

namespace UnreflectedSerializer
{
    /// <summary>
    /// Handles transition between describing an entity as an independent instance and assigning/retrieving the component value as a property of a parent class
    /// </summary>
    /// <typeparam name="TParent"></typeparam>
    /// <typeparam name="TComponent"></typeparam>
    internal class ComponentAdapter<TParent, TComponent> : IMemberDescriptor<TParent>
        where TComponent : new()
    {
        public ComponentAdapter(Expression<Func<TParent,TComponent>> component, IDescriptor<TComponent> descriptor, string propertyName = null)
            : this(component, propertyName)
        {
            componentDescriptor = descriptor;
        }

        public ComponentAdapter(Expression<Func<TParent, TComponent>> component, Action<IMapper<TComponent>> initialMapping, string propertyName = null)
            : this(component, propertyName)
        {
            var descriptor = new Descriptor<TComponent>();
            initialMapping(descriptor); //can invoke mapping on our defined Descriptor because it's also a Mapper
            componentDescriptor = descriptor;
        }

        private ComponentAdapter(Expression<Func<TParent, TComponent>> component, string propertyName)
        {
            PropertyName = propertyName ?? component.GetName();
            componentAccess = component.Compile();
            componentAssignment = component.ToSetter();
        }

        public string PropertyName { get; }
        private readonly IDescriptor<TComponent> componentDescriptor;
        private readonly Func<TParent, TComponent> componentAccess;
        private readonly Action<TParent, TComponent> componentAssignment;

        public void PopulateEntity(TParent targetEntity, TextReader reader)
        {
            using (componentDescriptor.AdaptToProperty(PropertyName))
            {
                TComponent deserializedComponent = componentDescriptor.Deserialize(reader);
                componentAssignment(targetEntity, deserializedComponent);
            }
        }

        public void SerializeMember(TParent sourceEntity, TextWriter writer, int tabsOffset = 0)
        {
            using (componentDescriptor.AdaptToProperty(PropertyName))
            {
                TComponent componentValue = componentAccess(sourceEntity);
                componentDescriptor.Serialize(componentValue, writer, tabsOffset);
            }
        }
    }
}