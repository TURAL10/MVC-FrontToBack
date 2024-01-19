namespace HomeTaskkMVC4.Models
{
    public class SaySliderImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }

        public bool IsMain { get; set; }
        public int SaySliderContentId { get; set; }

        public SaySliderContent SaySliderContent { get; set; }
    }
}
