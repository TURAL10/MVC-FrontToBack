namespace HomeTaskkMVC4.Models
{
    public class SaySliderContent
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        public string Profession { get; set; }

        public List<SaySliderImage> SaySliderImages { get; set; }

    }
}
