namespace HomeTaskkMVC4.Models.DemoEntities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BookGenres> BookGenres { get; set; }
    }
}
