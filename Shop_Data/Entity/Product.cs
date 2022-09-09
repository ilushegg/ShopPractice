

namespace Shop.Domain.Entity
{
    public class Product
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public string? Picture { get; set; }

        public DateTime? CreateDate { get; set; }

        public string? AuthorName { get; set; }

    }
}
