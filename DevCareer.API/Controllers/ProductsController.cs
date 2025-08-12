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
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _context.Products.Select(p => new ProductDto
            {
                Name = p.Name,
                Description = p.Description,
                Price = p.Price
            }).ToListAsync();

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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound($"Product with ID {id} not found");
            }

            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }
    }
}
