using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Seeds
{
    public static class ProductsContextSeed
    {
        public static async Task SeedAsync(BaseDbContext dbContext)
        {
            if (!dbContext.Products.Any())
            {
                dbContext.Products.AddRange(new Product[]
                    {
                        new("Product 1", 15),
                        new("Product 2", 23),
                        new("Product 3", 12, "This is product 3"),
                        new("Product 4", 20, "This is product 4"),
                        new("Product 5", 48, "Expensive prdouct")
                    });
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
