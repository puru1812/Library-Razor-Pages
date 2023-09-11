using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Library.Modals;

namespace Library.BooksData
{
    public class LibraryContext : DbContext
    {
        public LibraryContext (DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        public DbSet<Library.Modals.Book> Book { get; set; } = default!;

        public DbSet<Library.Modals.Customer> Customer { get; set; } = default!;
    }
}
