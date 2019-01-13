namespace SerializerConsoleTester
{
    internal class Planet
    {
        public double Mass { get; set; }
        public string Name { get; set; }
        public ChemicalElement MostCommonElement { get; set; }
    }

    internal class ChemicalElement
    {
        public char Id { get; set; }
        public byte AtomicNumberOrWhatever { get; set; }
    }
}
