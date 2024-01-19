namespace HomeTaskkMVC4.Models.DemoEntities
{
    public class BookGenres
    {
        public int Id { get; set; }
        public int BookId { get; set; }

        public Book Book { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
