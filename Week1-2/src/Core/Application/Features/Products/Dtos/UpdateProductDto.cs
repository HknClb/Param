namespace Application.Features.Products.Dtos
{
    public class UpdateProductDto
    {
        public UpdateProductDto()
        {
        }

        public UpdateProductDto(string id, string? name, string? description, decimal? unitPrice, bool isActive) : this()
        {
            Id = id;
            Name = name;
            Description = description;
            UnitPrice = unitPrice;
            IsActive = isActive;
        }

        public string Id { get; set; } = null!;
        public string? Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal? UnitPrice { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
