using AspNetCoreRepositoryPattern.Controllers.Base;
using AspNetCoreRepositoryPattern.Models;
using AspNetCoreRepositoryPattern.Models.Dtos;
using AspNetCoreRepositoryPattern.Models.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreRepositoryPattern.Controllers.V1;

/*
 * Not using repository pattern.
 * This is okay if you are not writing any tests because this is not testable.
 */
[ApiVersion("1.0")]
[Route("api/books")]
public class BooksController(ApplicationDbContext context, IMapper mapper) : ApiController
{
    // GET: api/books
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks(string author)
    {
        try
        {
            if (string.IsNullOrEmpty(author))
                return await AllBooksFromAllAuthors();
              
            return await BooksFromAnAuthor(author);
        }
        catch (Exception e)
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
        
    // GET: api/books/ab2bd817-98cd-4cf3-a80a-53ea0cd9c200
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<BookDto>> GetBook(Guid id)
    {
        try
        {
            var book = await context.Books.FindAsync(id);

            if (book == null)
                return NotFound();

            var bookDto = mapper.Map<BookDto>(book);
            return bookDto;
        }
        catch (Exception e)
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    // PUT: api/books/ab2bd817-98cd-4cf3-a80a-53ea0cd9c200
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> PutBook(Guid id, Book book)
    {
        if (id != book.Id)
            return BadRequest();

        context.Entry(book).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return !BookExists(id) 
                ? NotFound() 
                : new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        return NoContent();
    }

    // POST: api/books
    [HttpPost]
    public async Task<ActionResult<BookDto>> PostBook(Book book)
    {
        try
        {
            context.Books.Add(book);
            await context.SaveChangesAsync();
            var bookDto = mapper.Map<BookDto>(book);

            return CreatedAtAction("GetBook", new { id = bookDto.Id }, bookDto);
        }
        catch (Exception e)
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    // DELETE: api/books/ab2bd817-98cd-4cf3-a80a-53ea0cd9c200
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteBook(Guid id)
    {
        try
        {
            var book = await context.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            context.Books.Remove(book);
            await context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception e)
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    private bool BookExists(Guid id) => context.Books.Any(e => e.Id == id);
        
    private async Task<ActionResult<IEnumerable<BookDto>>> BooksFromAnAuthor(string author)
    {
        var books = await context
            .Books
            .Where(b => b.Author.Contains(author))
            .ToListAsync();

        if (books == null || books.Count == 0)
            return new List<BookDto>();

        return mapper.Map<List<BookDto>>(books);
    }
        
    private async Task<ActionResult<IEnumerable<BookDto>>> AllBooksFromAllAuthors()
    {
        var books = await context.Books.ToListAsync();
        var bookDtos = mapper.Map<List<BookDto>>(books);

        return bookDtos;
    }
}