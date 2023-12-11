using LibraryStoreApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryStoreApp.Utility
{
    public class AppDBContext : IdentityDbContext
    {

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) 
        {
            
        }

        public DbSet<BookType> BookTypes { get; set; }
        public DbSet<Book> Books { get; set; }

        public DbSet<Leasing> leasings { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }
    }
}
