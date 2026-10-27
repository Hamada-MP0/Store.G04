using Microsoft.AspNetCore.Mvc;
using Store.G04.Apis.Error;
using Store.G04.Core.Dtos.Products;
using Store.G04.Core.Entity;
using Store.G04.Core.HelperPage;
using Store.G04.Core.Services.Contract;
using Store.G04.Core.Specifications.Prodicts;

namespace Store.G04.Apis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : BaseApiController
    {
        private readonly IProductServices _productServices;

        public ProductsController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        /// <summary>
        /// Get all products with pagination, filtering, and sorting
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(PaginationsRESP<ProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PaginationsRESP<ProductDto>>> GetAllProducts(
            [FromQuery] ProductSpecParams specParams)
        {
            if (specParams.PageSize <= 0 || specParams.PageIndex <= 0)
                return BadRequest(new ApiErrorResponse(400, "PageSize and PageIndex must be greater than 0."));

            var products = await _productServices.GetAllProductsAsync(specParams);
            return Ok(products);
        }

        /// <summary>
        /// Get product by ID
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductById(int? id)
        {
            if (id == null || id <= 0)
                return BadRequest(new ApiErrorResponse(400, "Invalid Product ID."));

            var product = await _productServices.GetProductByIdAsync(id.Value);

            if (product is null)
                return NotFound(new ApiErrorResponse(404, $"Product with ID {id} not found."));

            return Ok(product);
        }

        /// <summary>
        /// Get all brands
        /// </summary>
        [HttpGet("brands")]
        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TypeBrandDto>>> GetAllBrands()
        {
            var brands = await _productServices.GetAllBrandsAsync();
            return Ok(brands);
        }

        /// <summary>
        /// Get all product types
        /// </summary>
        [HttpGet("types")]
        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TypeBrandDto>>> GetAllTypes()
        {
            var types = await _productServices.GetAllTypesAsync();
            return Ok(types);
        }

        /// <summary>
        /// Create a new product
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductDto>> AddProduct([FromBody] Product product)
        {
            if (product is null)
                return BadRequest(new ApiErrorResponse(400, "Product data is required."));

            if (!ModelState.IsValid)
                return BadRequest(new ApiErrorResponse(400, "Invalid product data."));

            if (string.IsNullOrWhiteSpace(product.Name))
                return BadRequest(new ApiErrorResponse(400, "Product name is required."));

            await _productServices.AddProductAsync(product);
            await _productServices.SaveChangesAsync();

            var productDto = await _productServices.GetProductByIdAsync(product.Id);

            return CreatedAtAction(
                nameof(GetProductById),
                new { id = product.Id },
                productDto);
        }

        /// <summary>
        /// Update an existing product
        /// </summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (product is null)
                return BadRequest(new ApiErrorResponse(400, "Product data is required."));

            if (!ModelState.IsValid)
                return BadRequest(new ApiErrorResponse(400, "Invalid product data."));

            if (id != product.Id)
                return BadRequest(new ApiErrorResponse(400, "Route ID and product ID do not match."));

            if (string.IsNullOrWhiteSpace(product.Name))
                return BadRequest(new ApiErrorResponse(400, "Product name is required."));

            var existingProduct = await _productServices.GetProductByIdAsync(id);

            if (existingProduct is null)
                return NotFound(new ApiErrorResponse(404, $"Product with ID {id} not found."));

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.PictureUrl = product.PictureUrl;
            existingProduct.BrandId = product.BrandId;
            existingProduct.TypeId = product.TypeId;

            await _productServices.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (id <= 0)
                return BadRequest(new ApiErrorResponse(400, "Invalid Product ID."));

            var product = await _productServices.GetProductByIdAsync(id);

            if (product is null)
                return NotFound(new ApiErrorResponse(404, $"Product with ID {id} not found."));

            await _productServices.DeleteProductAsync(new Product { Id = id });
            await _productServices.SaveChangesAsync();

            return NoContent();
        }
    }
}