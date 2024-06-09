using AzureBooks.Models;
using AzureBooks.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureBooks.Controllers;

[Route("authors")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly Context _context;

    private readonly HelperService _helperService;
    public AuthorController(Context context, HelperService helperService)
    {
        _context = context;
        context.Database.EnsureCreated();
        _helperService = helperService;
    }

    [HttpGet]
    public ActionResult<List<Author>> GetAuthors()
    {
        return _context.Authors.ToList();
    }

    [HttpGet("{authorId}")]
    public ActionResult<Author> GetAuthor(string authorId)
    {
        if (_helperService.CheckAuthorExists(authorId))
        {
            return BadRequest("Author doesn't exist!");
        }
        return _context.Authors.First(x => x.Id == authorId);
    }

    [HttpPost]
    public ActionResult PostAuthor(Author author)
    {
        if (_helperService.CheckAuthorExists(author.Id))
        {
            return BadRequest("Author exists!");
        }
        _context.Authors.Add(author);
        _context.SaveChanges();
        return Ok();
    }

    [HttpPut]
    public ActionResult PutAuthor(Author author)
    {
        _context.Entry(author).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        _context.SaveChanges();
        return Ok();
    }

    [HttpDelete("{authorId}")]
    public ActionResult PutAuthor(string authorId)
    {
        _context.Authors.Remove(_context.Authors.First(x => x.Id == authorId));
        _context.SaveChanges();
        return Ok();
    }
}
