namespace NezarkaController
{
    internal interface IParser<out T>
    {
        T Parse(string source);
    }
}
