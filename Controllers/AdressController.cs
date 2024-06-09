using AzureBooks.Models;
using AzureBooks.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureBooks.Controllers;

[Route("addresses")]
[ApiController]
public class AdressController : ControllerBase
{
    private readonly Context _context;

    private readonly HelperService _helperService;
    public AdressController(Context context, HelperService helperService)
    {
        _context = context;
        _helperService = helperService;
        context.Database.EnsureCreated();
    }

    [HttpGet]
    public ActionResult<List<Address>> GetAddresses()
    {
        return _context.Addresses.ToList();
    }

    [HttpGet("{addressId}")]
    public ActionResult<Address> GetAddress(string addressId)
    {
        if (_helperService.CheckAddressExists(addressId))
        {
             return BadRequest("Address doesn't exist!");
        }
        return _context.Addresses.First(x => x.Id == addressId);
    }

    [HttpPost]
    public ActionResult PostAddress(Address address)
    {
        if (_helperService.CheckAddressExists(address.Id))
        {
            return BadRequest("Address exists!");
        }
        _context.Database.EnsureCreated();
        _context.Addresses.Add(address);
        _context.SaveChanges();
        return Ok();
    }

    [HttpPut]
    public ActionResult PutAddress(Address address)
    {
        _context.Entry(address).State = EntityState.Modified;
        _context.SaveChanges();
        return Ok();
    }

    [HttpDelete("{addressId}")]
    public ActionResult DeleteAddress(string addressId)
    {
        var address = _context.Addresses.FirstOrDefault(x => x.Id == addressId);
        if (address == null)
        {
            return NotFound();
        }
        _context.Addresses.Remove(address);
        _context.SaveChanges();
        return Ok();
    }
}
