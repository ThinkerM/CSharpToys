namespace NezarkaModel
{
    public interface IProduct : IEntityBase, IViewable
    {
        string Name { get; }
        decimal Price { get; set; }
    }
}
