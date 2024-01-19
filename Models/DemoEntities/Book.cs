namespace HomeTaskkMVC4.Models.DemoEntities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public List<BookGenres> BookGenres { get; set; }
        public List<BookAuthor> BookAuthors {  get; set; }

        public Book()
        {
            BookAuthors = new List<BookAuthor>();
            BookGenres = new List<BookGenres>();
        }

    }
}
