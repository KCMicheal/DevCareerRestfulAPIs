using DevCareer.Data.Data;
using DevCareer.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevCareer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DevCareerDbContext _context;
        public ProductsController(DevCareerDbContext devCareerDbContext)
        {
            _context = devCareerDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _context.Products.Include(p => p.Category)
                                                  .Include(p => p.Tags)
                                                  .ToListAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException(ex.Message);
            }
            
        }
    }
}
