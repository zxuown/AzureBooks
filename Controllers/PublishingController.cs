using AzureBooks.Models;
using AzureBooks.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureBooks.Controllers;

[Route("publishings")]
[ApiController]
public class PublishingController : ControllerBase
{
    private readonly Context _context;

    private readonly HelperService _helperService;

    public PublishingController(Context context, HelperService helperService)
    {
        _context = context;
        _helperService = helperService;
        context.Database.EnsureCreated();
    }

    [HttpGet]
    public ActionResult<List<Publishing>> GetPublishings()
    {
        return _context.Publishings.ToList();
    }

    [HttpGet("{publishingId}")]
    public ActionResult<Publishing> GetPublishing(string publishingId)
    {
        if (_helperService.CheckPublishingExists(publishingId))
        {
            return BadRequest("Publishing doesn't exist!");
        }
        return _context.Publishings.First(x => x.Id == publishingId);
    }

    [HttpPost]
    public ActionResult PostPublishing(Publishing publishing)
    {
        if (_helperService.CheckPublishingExists(publishing.Id))
        {
            return BadRequest("Publishing exists!");
        }
        if (!_helperService.CheckPublishing(publishing))
        {
            return BadRequest("Invalid publishing details!");
        }
        _context.Database.EnsureCreated();
        _context.Publishings.Add(publishing);
        _context.SaveChanges();
        return Ok();
    }

    [HttpPut]
    public ActionResult PutPublishing(Publishing publishing)
    {
        if (_helperService.CheckPublishing(publishing))
        {
            return BadRequest("Invalid publishing details!");
        }
        _context.Entry(publishing).State = EntityState.Modified;
        _context.SaveChanges();
        return Ok();
    }

    [HttpDelete("{publishingId}")]
    public ActionResult DeletePublishing(string publishingId)
    {
        _context.Publishings.Remove(_context.Publishings.First(x => x.Id == publishingId));
        _context.SaveChanges();
        return Ok();
    }
}
