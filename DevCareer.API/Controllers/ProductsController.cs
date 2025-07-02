using DevCareer.Data.Data;
using DevCareer.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevCareer.API.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DevCareerDbContext _context;
        public ProductsController(DevCareerDbContext devCareerDbContext)
        {
            _context = devCareerDbContext;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var products = await _context.Products.Include(p => p.Category)
                                                  .Include(p => p.Tags)
                                                  .ToListAsync();
            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        [HttpGet("view/{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetAllProducts), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException(ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
            {
                return NotFound("Product not found");
            }

            existingProduct.Price = product.Price;
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;

            await _context.SaveChangesAsync();
            return Ok(existingProduct);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }
    }
}
