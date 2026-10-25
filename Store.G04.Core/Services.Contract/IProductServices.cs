using Store.G04.Core.Dtos.Products;
using Store.G04.Core.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.G04.Core.Services.Contract
{
    public interface IProductServices
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);

        Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync();
        Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync();
        

        Task AddProductAsync(Product product);

        Task DeleteProductAsync(Product product);
        
        Task SaveChangesAsync();
    }
}