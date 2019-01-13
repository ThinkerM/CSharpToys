using System;
using System.Linq.Expressions;

namespace UnreflectedSerializer.Mapping
{
    public interface IMapper<TEntity>
    {
        /// <summary>
        /// Register a single property, which will be subsequently serialized/deserialized using simple ToString/from string conversions
        /// </summary>
        /// <typeparam name="TValue">Type of the member which will be serialized</typeparam>
        /// <param name="property">Property access to the serialized member</param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        void Property<TValue>(Expression<Func<TEntity, TValue>> property, string propertyName = null);

        /// <summary>
        /// Register a nested component mapping using an existing <see cref="IDescriptor{TEntity}"/> instance. Useful for reusing the same descriptor for multiple components (of the same type).
        /// </summary>
        /// <typeparam name="TComponent">Type of the nested component. Must have a parameterless constructor for purposes of deserialization</typeparam>
        /// <param name="component"></param>
        /// <param name="componentDescriptor"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        void Component<TComponent>(Expression<Func<TEntity, TComponent>> component, IDescriptor<TComponent> componentDescriptor, string propertyName = null)
            where TComponent : new();

        /// <summary>
        /// Register a nested component mapping using an anonymous function. Useful for one-time mapping of a component, avoiding the need for creating
        /// a descriptor class for the component.
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <param name="component"></param>
        /// <param name="mapping"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        void Component<TComponent>(Expression<Func<TEntity, TComponent>> component, Action<IMapper<TComponent>> mapping, string propertyName = null)
            where TComponent : new();
    }
}
