using System;
using ReflectorSerializer;

namespace ConsoleTester
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var serializer = new Serializer();

            SingleIntTest(serializer);

            SingleStringTest(serializer);

            DefaultIndentTest(serializer, Person.TestInstance);

            CustomIndentTest(serializer, Person.TestInstance);

            CustomIndentEnumerableStringTest(serializer, Person.TestInstance);
        }

        private static void CustomIndentEnumerableStringTest(ISerializer serializer, Person person)
        {
            Console.WriteLine("INDENT BY 2 + STRING AS ENUMERABLE:");
            serializer.IndentSpaceCount = 2;
            serializer.TreatStringAsEnumerable = true;
            serializer.Serialize(Console.Out, person);
            Console.WriteLine();
        }

        private static void CustomIndentTest(ISerializer serializer, Person person)
        {
            Console.WriteLine("INDENT BY 4:");
            serializer.IndentSpaceCount = 4;
            serializer.Serialize(Console.Out, person);
            Console.WriteLine();
        }

        private static void SingleStringTest(ISerializer serializer)
        {
            Console.WriteLine("SINGLE STRING:");
            serializer.Serialize(Console.Out, "Hello");
            Console.WriteLine();
        }

        private static void DefaultIndentTest(ISerializer serializer, Person person)
        {
            Console.WriteLine("DEFAULT INDENT:");
            serializer.Serialize(Console.Out, person);
            Console.WriteLine();
        }

        private static void SingleIntTest(ISerializer serializer)
        {
            Console.WriteLine("SINGLE INT:");
            serializer.Serialize(Console.Out, 42);
            Console.WriteLine();
        }
    }
}
