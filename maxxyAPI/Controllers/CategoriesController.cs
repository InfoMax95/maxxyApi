using maxxyAPI.Data;
using maxxyAPI.DTOs;
using maxxyAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace maxxyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "RequireAdminRole")]
    public class CategoriesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly DataContext _context;
        public CategoriesController(IConfiguration configuration, DataContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetTypes()
        {
            var types = await _context.Categories.ToListAsync();
            return Ok(types);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetType([FromRoute] int id)
        {
            var type = await _context.Categories.FindAsync(id);
            return Ok(type);
        }

        [HttpPost("InsertType")]
        public async Task<ActionResult<Category>> InsertType(CategoryDto request)
        {
            Category _type = new Category();
            _type.Name = request.Name.ToLower();
            _type.Description = request.Description;

            _context.Categories.Add(_type);
            await _context.SaveChangesAsync();

            return Ok(_type);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<Category>> UpdateType([FromRoute] int id, Category _type)
        {
            if (id != _type.Id)
            {
                return BadRequest();
            }

            _context.Categories.Entry(_type).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(_type);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteType([FromRoute] int id)
        {
            var typeToDelete = await _context.Categories.FindAsync(id);
            if (typeToDelete == null)
                return BadRequest();

            _context.Categories.Remove(typeToDelete);
            await _context.SaveChangesAsync();

            return Ok(typeToDelete.Name);
        }
    }
}
