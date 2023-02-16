using Domain.Entities.Common;

namespace Domain.Entities
{
    public class Product : Entity
    {
        public Product()
        {
            Id = Guid.NewGuid();
        }

        public Product(string name, decimal unitPrice, string? description = null) : this()
        {
            Name = name;
            UnitPrice = unitPrice;
            Description = description;
        }

        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
