using Microsoft.EntityFrameworkCore;

namespace AzureBooks.Models;

public class Context : DbContext
{
    public Context(DbContextOptions options) : base(options) { }

    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<BookDto> BooksDto { get; set; }
    public virtual DbSet<Address> Addresses { get; set; }
    public virtual DbSet<Author> Authors { get; set; }
    public virtual DbSet<Publishing> Publishings { get; set; }
}
