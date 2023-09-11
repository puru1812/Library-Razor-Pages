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
    public class DetailsModel : PageModel
    {
        private readonly Library.BooksData.LibraryContext _context;

        public DetailsModel(Library.BooksData.LibraryContext context)
        {
            _context = context;
        }

      public Customer Customer { get; set; } = default!;
        public IList<Book> Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Customer == null)
            {
                return NotFound();
            }
            Book = await _context.Book.ToListAsync();
            var customer = await _context.Customer.FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            else 
            {
                Customer = customer;
            }
            return Page();
        }
    }
}
