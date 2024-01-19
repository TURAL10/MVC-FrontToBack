using System.ComponentModel.DataAnnotations;

namespace HomeTaskkMVC4.ViewModels.AdminProduct
{
    public class UpdateProductVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public double Price { get; set; }
        public int CategoryId { get; set; }

        public int Count { get; set; }

        public string ImageUrl { get; set; }
        public IFormFile[] Photos { get; set; }
    }
}
