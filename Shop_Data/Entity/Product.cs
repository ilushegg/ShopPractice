﻿

namespace Shop.Domain.Entity
{
    public class Product
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Picture { get; set; }
    }
}
