using AzureBooks.Models;
using AzureBooks.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AzureBooks.Controllers;

[Route("books")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly Context _context;

    private readonly HelperService _helperService;

    public BookController(Context context, HelperService helperService)
    {
        _context = context;
        _helperService = helperService;
        context.Database.EnsureCreated();
    }

    [HttpGet]
    public ActionResult<List<Book>> GetBooks()
    {
        return _context.Books.ToList();
    }

    [HttpGet("{bookId}/authors")]
    public ActionResult<List<BookDto>> GetBookAuthors(string bookId)
    {
        if (!_helperService.CheckBookExists(bookId))
        {
            return BadRequest("Book doesn't exist!");
        }
        return _context.BooksDto.Where(x => x.BookId == bookId).ToList();
    }

    [HttpPost("authors")]
    public ActionResult PostBookAuthor(BookDto bookDto)
    {
        if (_helperService.CheckBookDtoExists(bookDto.Id))
        {
            return BadRequest("BookDto exist!");
        }

        if (!_helperService.CheckBookDto(bookDto))
        {
            return BadRequest("Invalid bookDto details!");
        }

        _context.BooksDto.Add(bookDto);
        _context.SaveChanges();
        return Ok();
    }

    [HttpGet("{bookId}")]
    public ActionResult<Book> GetBook(string bookId)
    {
        if (!_helperService.CheckBookExists(bookId))
        {
            return BadRequest("Book doesn't exist!");
        }
        return _context.Books.First(x => x.Id == bookId);
    }

    [HttpPost]
    public ActionResult PostBook(Book book)
    {
        if (_helperService.CheckBookExists(book.Id))
        {
            return BadRequest("Book exists!");
        }

        if (!_helperService.CheckBook(book))
        {
            return BadRequest("Invalid book details!");
        }

        _context.Books.Add(book);
        _context.SaveChanges();
        return Ok();
    }

    [HttpPut]
    public ActionResult PutBook(Book book)
    {
        if (!_helperService.CheckBook(book))
        {
            return BadRequest("Invalid book details!");
        }
        _context.Entry(book).State = EntityState.Modified;
        _context.SaveChanges();
        return Ok();
    }

    [HttpDelete("{bookId}")]
    public ActionResult DeleteBook(string bookId)
    {
        Book book = _context.Books.First(x => x.Id == bookId);
        if (!_helperService.CheckBook(book))
        {
            return BadRequest("Invalid book details!");
        }
        _context.Books.Remove(book);
        _context.SaveChanges();
        return Ok();
    }
}
