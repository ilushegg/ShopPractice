using Shop.Domain.Enum;

namespace Shop.Domain.Entity
{
    public class User
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }

        public long? CartId { get; set; }

        public Cart? Cart { get; set; }

    }
}
