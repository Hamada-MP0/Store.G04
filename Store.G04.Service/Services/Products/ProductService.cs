using AutoMapper;
using Store.G04.Core;
using Store.G04.Core.Dtos.Products;
using Store.G04.Core.Entity;
using Store.G04.Core.Services.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.G04.Service.Services.Products
{
    public class ProductService : IProductServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.Repository<Product, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.Repository<Product, int>().GetByIdAsync(id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<TypeBrandDto>>(brands);
        }

        public async Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.Repository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<TypeBrandDto>>(types);
        }

        public async Task AddProductAsync(Product product)
        {
            await _unitOfWork.Repository<Product, int>().AddAsync(product);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteProductAsync(Product product)
        {
            _unitOfWork.Repository<Product, int>().Delete(product);
            await _unitOfWork.CommitAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _unitOfWork.CommitAsync();
        }
    }
}