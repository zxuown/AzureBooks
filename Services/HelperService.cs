using AzureBooks.Models;
using Microsoft.EntityFrameworkCore;

namespace AzureBooks.Services;

public class HelperService
{
    private readonly Context _context;

    public HelperService(Context context)
    {
        _context = context;
        context.Database.EnsureCreated();
    }

    public bool CheckBookExists(string bookId)
    {
        if (_context.Books.FirstOrDefault(x => x.Id == bookId) == null)
        {
            return false;
        }
        return true;
    }
    public bool CheckBook(Book book)
    {
        if (_context.Addresses.FirstOrDefault(x => x.Id == book.AdressId) == null || _context.Publishings.FirstOrDefault(x => x.Id == book.PublishingId) == null)
        {
            return false;
        }
        return true;
    }
    public bool CheckPublishingExists(string publishingId)
    {
        if (_context.Publishings.FirstOrDefault(x => x.Id == publishingId) == null)
        {
            return false;
        }
        return true;
    }
    public bool CheckBookDto(BookDto bookDto)
    {
        if (_context.Books.FirstOrDefault(x=>x.Id == bookDto.BookId) == null || _context.Authors.FirstOrDefault(x => x.Id == bookDto.AuthorId) == null)
        {
            return false;
        }
        return true;
    }
    public bool CheckBookDtoExists(string bookDtoId)
    {
        if (_context.BooksDto.FirstOrDefault(x => x.Id == bookDtoId) == null)
        {
            return false;
        }
        return true;
    }
    public bool CheckPublishing(Publishing publishing)
    {
        var temp = _context.Addresses.FirstOrDefault(x => x.Id == publishing.AdressId);
        if (_context.Addresses.FirstOrDefault(x=> x.Id == publishing.AdressId) == null)
        {
            return false;
        }
        return true;
    }

    public bool CheckAddressExists(string addressId)
    {
        if (_context.Addresses.FirstOrDefault(x => x.Id == addressId) == null)
        {
            return false;
        }
        return true;
    }

    public bool CheckAuthorExists(string authorId)
    {
        if (_context.Authors.FirstOrDefault(x => x.Id == authorId) == null)
        {
            return false;
        }
        return true;
    }
}
