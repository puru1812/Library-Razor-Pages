using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Library.BooksData;
using Library.Modals;

namespace Library.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly Library.BooksData.LibraryContext _context;

        public IndexModel(Library.BooksData.LibraryContext context)
        {
            _context = context;
        }

        public IList<Customer> Customer { get;set; } = default!;
        public IList<Book> Book { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Customer != null)
            {
                Customer = await _context.Customer.ToListAsync();
                Book = await _context.Book.ToListAsync();
            }
        }
    }
}
