using Book_AuthorWIthAuthenticationWebApi.DBContext;
using Book_AuthorWIthAuthenticationWebApi.DTOs;
using Book_AuthorWIthAuthenticationWebApi.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Book_AuthorWIthAuthenticationWebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            return await _context.Authors.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);

            if (author is null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        // POST: api/authors
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(AuthorDto authorDto)
        {
            var author = new Author
            {
                Name = authorDto.Name,
                BirthDate = authorDto.BirthDate,
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
        }

        // PUT: api/authors/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, Author author)
        {
            if (id != author.Id) return BadRequest();

            _context.Entry(author).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/authors/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return NotFound();

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
