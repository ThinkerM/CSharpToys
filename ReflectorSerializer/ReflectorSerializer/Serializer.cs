using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ReflectorSerializer
{
    public class Serializer : ISerializer
    {
        private IPrimitivenessChecker PrimitivenessChecker { get; set; }
        private bool IsPrimitive(Type type) => PrimitivenessChecker.IsPrimitive(type);

        public Serializer(int defaultIndentSpaces = 2, IPrimitivenessChecker defaultPrimitiveness = null)
        {
            IndentSpaceCount = defaultIndentSpaces;
            PrimitivenessChecker = defaultPrimitiveness ?? new SystemAndStringPrimitiveness();
            TreatStringAsEnumerable = false;
        }

        public int IndentSpaceCount { private get; set; }
        private int SpacesOffset(int recursionDepth) => recursionDepth * IndentSpaceCount;

        public bool TreatStringAsEnumerable
        {
            set
            {
                if (value) PrimitivenessChecker = new SystemTypesPrimitiveness();
                else PrimitivenessChecker = new SystemAndStringPrimitiveness();
            }
        }

        void ISerializer.Serialize(TextWriter output, object entity)
        {
            SerializeRoot(output, entity, 0);
        }

        private void SerializeRoot(TextWriter output, object entity, int initialDepth)
        {
            Serialize(output, entity, entity.GetType().Name, initialDepth, isRoot: true);
        }

        private void Serialize(TextWriter output, object entity, string entityName, int recursionDepth, bool isRoot)
        {
            Type entityType = entity.GetType();

            if (IsPrimitive(entityType))
            {
                SerializePrimitive(output, entity, entityName , recursionDepth);
                return;
            }

            output.WriteLineOffset(entityName.ToXmlOpeningTag(), SpacesOffset(recursionDepth));

            if (entity is IEnumerable enumerable)
            {
                SerializeEnumerableContents(output, enumerable, recursionDepth + 1);
            }
            else
            {
                SerializeProperties(output, entity, recursionDepth + 1, isRoot);
            }
            output.WriteLineOffset(entityName.ToXmlClosingTag(), SpacesOffset(recursionDepth));
        }

        private void SerializeProperties(TextWriter output, object entity, int recursionDepth, bool isRoot)
        {
            Type entityType = entity.GetType();
            if (!isRoot)
                output.WriteLineOffset(entityType.Name.ToXmlOpeningTag(), SpacesOffset(recursionDepth));

            foreach (PropertyInfo property in entityType.GetProperties(BindingFlags.Instance | BindingFlags.Public)) //ignore static properties
            {
                object propertyValue = property.GetValue(entity);
                int nextRecursionDepth = isRoot ? recursionDepth : recursionDepth + 1;
                Serialize(output, propertyValue, property.Name, nextRecursionDepth, isRoot: false);
            }

            if (!isRoot)
                output.WriteLineOffset(entityType.Name.ToXmlClosingTag(), SpacesOffset(recursionDepth));
        }

        private void SerializeEnumerableContents(TextWriter output, IEnumerable enumerable, int recursionDepth)
        {
            foreach (object entity in enumerable)
            {
                SerializeRoot(output, entity, recursionDepth); //items in collection are treeated as roots (otherwise redundant tags would be created)
            }
        }

        private void SerializePrimitive(TextWriter output, object primitiveEntity, string propertyTag, int recursionDepth)
        {
            output.WriteLineOffset(propertyTag.ToXmlOpeningTag() +
                             primitiveEntity +
                             propertyTag.ToXmlClosingTag(),
                             offsetCount: SpacesOffset(recursionDepth));
        }
    }
}