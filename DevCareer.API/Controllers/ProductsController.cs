using DevCareer.Data.Data;
using DevCareer.Data.Models;
using DevCareer.Data.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevCareer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public async Task<IActionResult> CreateProduct(ProductDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (model == null)
                {
                    return BadRequest("Product data is null.");
                }
                Category category = new Category
                {
                    Name = "Default Category",
                    Description = "Default Category Description"
                };
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();

                Product product = new Product
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    CategoryId = category.Id,
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException(ex.Message);
            }

        }
    }
}
