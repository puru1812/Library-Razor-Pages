using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Library.Modals;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;
using static System.Net.Mime.MediaTypeNames;

namespace Library.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly Library.BooksData.LibraryContext _context;

        public CreateModel(Library.BooksData.LibraryContext context)
        {
            _context = context;
        }
        public SelectList? Customers { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            if (_context.Customer != null)
            {
                IQueryable<string> customerQuery = from s in _context.Customer
                                                   orderby s.Name
                                                   select s.Name;
                Customers = new SelectList(await customerQuery.ToListAsync());
            }
            return Page();
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? CustomerName { get; set; }
        public IList<Customer> customers { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD

        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Book == null || Book == null)
            {
                return Page();
            }


        
            var finalList = _context.Customer.Where(x => x.Name.Contains(CustomerName) );


            customers=await finalList.ToListAsync();

            Book.CustomerKey = customers[0];
            customers[0].IssuedBook = Book;
            _context.Attach(customers[0]).State = EntityState.Modified;
            _context.Book.Add(Book);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
