using Microsoft.EntityFrameworkCore;
using BookVerse.Domain.Entities;

namespace BookVerse.Infrastructure.Data
{
public class BookVerseDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public BookVerseDbContext(DbContextOptions<BookVerseDbContext> options) : base(options)
    {
        
    }
}
}