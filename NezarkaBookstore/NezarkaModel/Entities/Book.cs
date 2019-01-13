namespace NezarkaModel.Entities
{
    public class Book : ICopyrightable
    {
        public Book(int id, string title, string author, decimal price)
        {
            Id = id;
            Title = title;
            Author = author;
            Price = price;
        }

        public int Id { get; }
        public int ViewId => Id;

        public string Title { get; }
        public string Author { get; }

        public decimal Price { get; set; }
        public string Name => Title;
    }
}