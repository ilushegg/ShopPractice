
namespace Shop.Domain.Entity
{
    public class Cart
    {
        public long Id { get; set; }

        public long ProductId { get; set; }

        public Product? Product { get; set; }

    }
}
