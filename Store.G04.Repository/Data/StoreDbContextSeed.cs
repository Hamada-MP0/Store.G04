using Store.G04.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;


namespace Store.G04.Repository.Data
{
    public class StoreDbContextSeed
    {
        public async static Task SeedAsync(Context.StoreDbContext _context)
        {
            if (_context.Brands.Count() == 0)
            {
                var brandsData = File.ReadAllText(@"..\Store.G04.Repository\Data\DataSeed\brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if (brands is not null && brands.Count() > 0)
                {
                    await _context.Brands.AddRangeAsync(brands);
                  
                }
            }
            if (_context.Types.Count() == 0)
            {
                var typesData = File.ReadAllText(@"..\Store.G04.Repository\Data\DataSeed\types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                if (types is not null && types.Count() > 0)
                {
                    await _context.Types.AddRangeAsync(types);
               
                }
            }
            if (_context.Products.Count() == 0)
            {
                var productsData = File.ReadAllText(@"..\Store.G04.Repository\Data\DataSeed\products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                if (products is not null && products.Count() > 0)
                {
                    await _context.Products.AddRangeAsync(products);
                 
                }


            }
            await _context.SaveChangesAsync();
        }
    }
}
