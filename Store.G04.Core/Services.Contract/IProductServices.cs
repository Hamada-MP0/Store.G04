using Store.G04.Core.Dtos.Products;
using Store.G04.Core.Entity;
using Store.G04.Core.HelperPage;
using Store.G04.Core.Specifications.Prodicts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.G04.Core.Services.Contract
{
    public interface IProductServices
    {
        Task<PaginationsRESP<ProductDto>> GetAllProductsAsync(ProductSpecParams specParams);
        Task<ProductDto> GetProductByIdAsync(int id);

        Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync();
        Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync();
        

        Task AddProductAsync(Product product);

        Task DeleteProductAsync(Product product);
        
        Task SaveChangesAsync();
    }
}