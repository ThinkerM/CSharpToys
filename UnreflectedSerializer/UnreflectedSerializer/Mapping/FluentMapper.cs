using System;
using System.Linq.Expressions;

namespace UnreflectedSerializer.Mapping
{
    public abstract class FluentMapper<TEntity, TDerived> : Mapper<TEntity>
        where TDerived : FluentMapper<TEntity, TDerived> //returning TDerived so that inheriting classes return their own type and not their parent (FluentMapper)
    {
        private TDerived Self => (TDerived)this;

        public TDerived WithProperty<TValue>(Expression<Func<TEntity, TValue>> property, string propertyName = null)
        {
            base.Property(property, propertyName);
            return Self;
        }

        public TDerived WithComponent<TComponent>(Expression<Func<TEntity, TComponent>> component, IDescriptor<TComponent> componentDescriptor, string propertyName = null)
            where TComponent : new()
        {
            base.Component(component, componentDescriptor, propertyName);
            return Self;
        }

        public TDerived WithComponent<TComponent>(Expression<Func<TEntity, TComponent>> component, Action<IMapper<TComponent>> mapping, string propertyName = null)
            where TComponent : new()
        {
            base.Component(component, mapping, propertyName);
            return Self;
        }
    }
}