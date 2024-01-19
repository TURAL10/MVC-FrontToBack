﻿namespace HomeTaskkMVC4.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public double Price { get; set; }
        public int CategoryId { get; set; }

        public int Count { get; set; }
        public Category Category { get; set; }

        public List<ProductImage> ProductImages { get; set; }

        public Product() { 
            ProductImages = new List<ProductImage>();
        }

    }
}
