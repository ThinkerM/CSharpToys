namespace NezarkaModel
{
    /// <summary>
    /// An independent individual
    /// </summary>
    public interface IPerson : IEntityBase
    {
        string FirstName { get; }
        string LastName { get; }
    }
}
