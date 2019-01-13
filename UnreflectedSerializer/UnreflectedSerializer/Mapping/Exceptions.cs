using System;

namespace UnreflectedSerializer.Mapping
{
    public class MappingException : Exception
    {
        protected readonly Type ParentEntityType;
        protected readonly string PropertyName;
        protected MappingException(Type parentEntityType, string propertyName)
        {
            ParentEntityType = parentEntityType;
            PropertyName = propertyName;
        }
    }

    public class MissingSetterException : MappingException
    {
        public MissingSetterException(Type parentEntityType, string propertyName) : base(parentEntityType, propertyName) { }
        public override string Message 
            => $"No setter for property '{this.PropertyName}' in '{this.ParentEntityType}'. All mapped properties must have a setter for purposes of deserialization.";
    }

    public class DuplicateMappingException : MappingException
    {
        public DuplicateMappingException(Type parentEntityType, string propertyName) : base(parentEntityType, propertyName) { }
        public override string Message
            => $"Property '{this.PropertyName}' of type '{this.ParentEntityType}' has already been mapped.";
    }
}
