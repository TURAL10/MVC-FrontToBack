using HomeTaskkMVC4.Models;

namespace HomeTaskkMVC4.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public SliderContent SliderContents { get; set; }

        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
        public AboutContent AboutContent { get; set; }
        public List<AboutListContent> AboutListContent { get; set; }

        public ExpertContent ExpertContent { get; set; }

        public List<Experts> Experts { get; set; }

        public BlogContent BlogContent { get; set; }

        public List<BlogImages> BlogImages { get; set; }

        public List<SaySliderContent>SaySliderContents { get; set; }    
        public List<InstagramSlider> InstagramSliders { get; set; }
       

    }
}
