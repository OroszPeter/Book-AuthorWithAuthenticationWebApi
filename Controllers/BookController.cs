using Book_AuthorWIthAuthenticationWebApi.DBContext;
using Book_AuthorWIthAuthenticationWebApi.DTOs;
using Book_AuthorWIthAuthenticationWebApi.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController(ApplicationDbContext context) : ControllerBase
    {
        [Authorize]
        [HttpPost("create")]
        public async Task<ActionResult<Book>> CreateBook(BookDto bookDto)
        {
            var author = await context.Authors.FindAsync(bookDto.AuthorId);
            if (author == null)
            {
                return NotFound("A megadott író nem létezik");
            }

            var book = new Book
            {
                Title = bookDto.Title,
                PublishedDate = bookDto.PublishedDate,
                AuthorId = bookDto.AuthorId
            };

            context.Books.Add(book);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }


        [Authorize]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await context.Books.ToListAsync();

            return Ok(books);
        }

        [Authorize]
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await context.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (book is null)
            {
                return NotFound("A könyv nem található!");
            }

            return Ok(book);
        }

        [Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var bookToDelete = await context.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (bookToDelete is null)
            {
                return NotFound("A könyv nem található!");
            }

            context.Books.Remove(bookToDelete);
            await context.SaveChangesAsync();

            return Ok("Sikeres törlés!");
        }

        [Authorize]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, Book updatedBook)
        {
            var bookToUpdate = await context.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (bookToUpdate is null)
            {
                return NotFound("A könyv nem található!");
            }

            bookToUpdate.Author = updatedBook.Author;
            bookToUpdate.Title = updatedBook.Title;
            bookToUpdate.PublishedDate = updatedBook.PublishedDate;
            await context.SaveChangesAsync();

            return Ok("Sikeres módosítás!");
        }


    }
}
