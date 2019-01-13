using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UnreflectedSerializer.Mapping
{
    public abstract class Mapper<TEntity> : IMapper<TEntity>
    {
        protected readonly IDictionary<string, IMemberDescriptor<TEntity>> MappedProperties = new Dictionary<string, IMemberDescriptor<TEntity>>();

        private void MapMember(IMemberDescriptor<TEntity> memberDescriptor)
        {
            if (MappedProperties.ContainsKey(memberDescriptor.PropertyName))
                throw new DuplicateMappingException(typeof(TEntity), memberDescriptor.PropertyName);

            MappedProperties.Add(memberDescriptor.PropertyName, memberDescriptor);
        }

        public void Property<TValue>(Expression<Func<TEntity, TValue>> property, string propertyName = null)
        {
            var mappedProperty = new SinglePropertyDescriptor<TEntity, TValue>(property, propertyName);
            MapMember(mappedProperty);
        }

        public void Component<TComponent>(Expression<Func<TEntity, TComponent>> component, IDescriptor<TComponent> componentDescriptor, string propertyName = null)
            where TComponent : new()
        {
            var mappedComponent = new ComponentAdapter<TEntity, TComponent>(component, componentDescriptor, propertyName);
            MapMember(mappedComponent);
        }

        public void Component<TComponent>(Expression<Func<TEntity, TComponent>> component, Action<IMapper<TComponent>> mapping, string propertyName = null)
            where TComponent : new()
        {
            var mappedComponent = new ComponentAdapter<TEntity, TComponent>(component, mapping, propertyName);
            MapMember(mappedComponent);
        }
    }
}
