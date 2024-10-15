using QuickCart.Domain.Entities;
using System.Text.Json;

namespace QuickCart.Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(QuickCartDbContext context)
        {
            if(!context.Products.Any())
            {
                var productsData = await File.ReadAllTextAsync("../QuickCart.Infrastructure/Data/SeedData/products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products == null)
                    return;

                context.Products.AddRange(products);

                await context.SaveChangesAsync();
            }
        }
    }
}
