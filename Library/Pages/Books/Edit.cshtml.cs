using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.BooksData;
using Library.Modals;

namespace Library.Pages.Books
{
    public class EditModel : PageModel
    {
        private readonly Library.BooksData.LibraryContext _context;

        public EditModel(Library.BooksData.LibraryContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? CustomerName { get; set; }
        public SelectList? Customers { get; set; }

        public IList<Customer> customers { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book =  await _context.Book.FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }
          
            Book = book;
            if (_context.Customer != null)
            {
                IQueryable<string> customerQuery = from s in _context.Customer
                                                   orderby s.Name
                                                   select s.Name;
                Customers = new SelectList(await customerQuery.ToListAsync());
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var finalList = _context.Customer.Where(x => x.Name.Contains(CustomerName));


            customers = await finalList.ToListAsync();

            Book.CustomerKey = customers[0];

            customers[0].IssuedBook= Book;
            _context.Attach(customers[0]).State = EntityState.Modified;

            _context.Attach(Book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(Book.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BookExists(int id)
        {
          return (_context.Book?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
