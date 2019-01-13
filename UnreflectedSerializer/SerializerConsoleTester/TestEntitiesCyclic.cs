namespace SerializerConsoleTester
{
    internal class Human
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    internal class Servant : Human
    {
        public King Liege { get; set; }
    }

    internal class King : Human
    {
        public Servant Servant { get; set; }
    }
}
