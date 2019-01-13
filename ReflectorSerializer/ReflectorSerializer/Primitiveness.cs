using System;

namespace ReflectorSerializer
{
    public interface IPrimitivenessChecker
    {
        bool IsPrimitive(Type type);
    }

    internal class SystemTypesPrimitiveness : IPrimitivenessChecker
    {
        public bool IsPrimitive(Type type) => type.IsPrimitive;
    }

    internal class SystemAndStringPrimitiveness : IPrimitivenessChecker
    {
        public bool IsPrimitive(Type type) => type.IsPrimitive || type == typeof(string);
    }
}
