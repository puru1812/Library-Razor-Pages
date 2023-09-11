using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Library.Modals;
using static System.Reflection.Metadata.BlobBuilder;

namespace Library.Pages.Customers
{
    public class EditModel : PageModel
    {
        private readonly Library.BooksData.LibraryContext _context;

        public EditModel(Library.BooksData.LibraryContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        public SelectList? Books { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            
            if (id == null || _context.Customer == null)
            {
                return NotFound();
            }

            var customer =  await _context.Customer.FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            Customer = customer;
            if (_context.Book != null)
            {
                IQueryable<string> booksQuery = from s in _context.Book
                                                orderby s.Title
                                                select s.Title;
                Books = new SelectList(await booksQuery.Distinct().ToListAsync());
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
           
     
            _context.Attach(Customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(Customer.Id))
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

        private bool CustomerExists(int id)
        {
          return (_context.Customer?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
