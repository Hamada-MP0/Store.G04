using AutoMapper;
using Store.G04.Core;
using Store.G04.Core.Dtos.Products;
using Store.G04.Core.Entity;
using Store.G04.Core.HelperPage;
using Store.G04.Core.Services.Contract;
using Store.G04.Core.Specifications;
using Store.G04.Core.Specifications.Prodicts;
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

        public async Task<PaginationsRESP<ProductDto>> GetAllProductsAsync(ProductSpecParams specParams)
        {
            var Spec = new ProductSpecification(specParams);
            var products = await _unitOfWork.Repository<Product, int>().GetAllًWithSpecAsync(Spec);
            var mapProoduct = _mapper.Map<IEnumerable<ProductDto>>(products);
            var countSpec = new ProductWithCountSpecification(specParams);
            var count = await _unitOfWork.Repository<Product, int>().CountAsync(countSpec);

            return new PaginationsRESP<ProductDto>(specParams.PageIndex, specParams.PageSize, count, mapProoduct);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var Spec = new ProductSpecification(id);
            var product = await _unitOfWork.Repository<Product, int>().GetByIdWithSpecAsync(Spec);
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