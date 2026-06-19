using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ItemsAPI.Data;
using ItemsAPI.Models;

namespace ItemsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SqlStudentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SqlStudentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Students.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentSql student)
        {
            _context.Students.Add(student);

            await _context.SaveChangesAsync();

            return Ok(student);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var student =
                await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}