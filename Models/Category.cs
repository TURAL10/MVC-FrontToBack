﻿namespace HomeTaskkMVC4.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Desc { get; set; }

        public bool IsDeleted { get; set; }
        public List<Product> Products { get; set; }
    }
}
