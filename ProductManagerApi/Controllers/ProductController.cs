using Microsoft.AspNetCore.Mvc;
using ProductManagerApi.Models;
using ProductManagerApi.Repository;

namespace ProductManagerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductModel>>> GetAll()
        {
            var products = await _productRepository.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> GetById(int id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product == null)
                {
                    return NotFound($"There is no product with this ID");
                }
                return Ok(product);
            }
            catch (Exception)
            {
                return BadRequest("Invalid request");
            }
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<ProductModel>> GetByName(string name)
        {
            try
            {
                var product = await _productRepository.GetByNameAsync(name);
                if (product == null)
                {
                    return NotFound($"There is no product with this name");
                }
                return Ok(product);
            }
            catch (Exception)
            {
                return BadRequest("Invalid request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProductModel>> AddProduct([FromBody] ProductModel product)
        {
            if (product == null)
            {
                return BadRequest("Product is null");
            }
            try
            {
                var existingProduct = await _productRepository.GetByNameAsync(product.Name);
                if (existingProduct != null)
                {
                    return Conflict("Product with this name already exists");
                }
                var createdProduct = await _productRepository.AddAsync(product);
                return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
            }
            catch (Exception)
            {
                return BadRequest("Invalid request");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductModel>> UpdateProduct(int id, [FromBody] ProductModel product)
        {
            if (product == null)
            {
                return BadRequest("Product is null");
            }
            try
            {
                var existingProduct = await _productRepository.GetByIdAsync(id);
                if (existingProduct == null)
                {
                    return NotFound($"There is no product with this ID");
                }
                product.Id = id;
                var updatedProduct = await _productRepository.UpdateAsync(product);
                return Ok(updatedProduct);
            }
            catch (Exception)
            {
                return BadRequest("Invalid request");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteProduct(int id)
        {
            try
            {
                var existingProduct = await _productRepository.GetByIdAsync(id);
                if (existingProduct == null)
                {
                    return NotFound($"There is no product with this ID");
                }
                var result = await _productRepository.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Invalid request");
            }
        }
    }
}
