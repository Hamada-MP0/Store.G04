
using Microsoft.AspNetCore.Mvc;
using Store.G04.Core.Dtos.Products;
using Store.G04.Core.Entity;
using Store.G04.Core.HelperPage;
using Store.G04.Core.Services.Contract;
using Store.G04.Core.Specifications.Prodicts;

namespace Store.G04.Apis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductsController(IProductServices productServices)
        {
            _productServices = productServices;
        } 
        //products/             ,,,  sort with Name | price 
        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery]ProductSpecParams specParams)
        {
            var products = await _productServices.GetAllProductsAsync(specParams);
            return Ok(products);
        }
        //products/id
        [HttpGet("{id:int}")]  
        public async Task<ActionResult<ProductDto>>  GetProductById(int id)
        {
            var product =  await _productServices.GetProductByIdAsync(id);

            if (product is null)
                return NotFound();

            return Ok(product);
        }

        //products/brands
        [HttpGet("brands")] 
        public async Task<ActionResult> GetAllBrands()
        {
            var brands = await _productServices.GetAllBrandsAsync();
            return Ok(brands);
        }

        //products/types

        [HttpGet("types")] 
        public async Task<ActionResult> GetAllTypes()
        {
            var brands = await _productServices.GetAllTypesAsync();
            return Ok(brands);
        }


        // Post Product

        [HttpPost]
        public async Task<ActionResult<ProductDto>>
            AddProduct([FromBody] Product product)
        {
            if (product is null)
                return BadRequest();

            await _productServices.AddProductAsync(product);
            await _productServices.SaveChangesAsync();

            var productDto =
                await _productServices.GetProductByIdAsync(product.Id);

            return CreatedAtAction(
                nameof(GetProductById),
                new { id = product.Id },
                productDto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(
            int id,
            [FromBody] Product product)
        {
            if (product is null)
                return BadRequest();

            if (id != product.Id)
                return BadRequest("Route ID and product ID do not match.");

            var existingProduct =
                await _productServices.GetProductByIdAsync(id);

            if (existingProduct is null)
                return NotFound();

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.PictureUrl = product.PictureUrl;
            existingProduct.BrandId = product.BrandId;
            existingProduct.TypeId = product.TypeId;

            await _productServices.SaveChangesAsync();

            return NoContent();
        }

        
    }
}
