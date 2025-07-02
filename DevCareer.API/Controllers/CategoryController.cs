using System;
using DevCareer.Data.Data;
using DevCareer.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevCareer.API.Controllers;

public class CategoryController
{
    [Route("api/category")]
    [ApiController]
    public class CartController(DevCareerDbContext devCareerDbContext) : ControllerBase
    {
        private readonly DevCareerDbContext _context = devCareerDbContext;

        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategory()
        {
            var categories = await _context.Categories.Include(p => p.Products).ToListAsync();

            if (categories == null)
            {
                return NotFound();
            }

            return Ok(categories);
        }

        [HttpGet("view/{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateCategory([FromBody] Category category)
        {
            try
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetAllCategory), new { id = category.Id }, category);
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException(ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category category)
        {
            var existingCategory = await _context.Categories.FindAsync(id);
            if (existingCategory == null)
            {
                return NotFound("Product not found");
            }

            existingCategory.Description = category.Description;
            existingCategory.Name = category.Name;

            await _context.SaveChangesAsync();
            return Ok(existingCategory);
        }
        
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return Ok(category);
        }
    }
}
