namespace NezarkaModel
{
    /// <summary>
    /// Product under copyright laws - has an official creator and artistic title
    /// </summary>
    public interface ICopyrightable : IProduct
    {
        string Author { get; }
        string Title { get; }
    }
}
