using HomeTaskkMVC4.Models;

namespace HomeTaskkMVC4.ViewModels
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public double Price { get; set; }
        public Category Category { get; set; }
    }
}
