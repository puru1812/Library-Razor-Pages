using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Library.BooksData;
using Library.Modals;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace Library.Pages.Customers
{
    public class CreateModel : PageModel
    {
        private readonly Library.BooksData.LibraryContext _context;

        public CreateModel(Library.BooksData.LibraryContext context)
        {
            _context = context;
        }
        [BindProperty(SupportsGet = true)]
        public string? BookName { get; set; }
        public SelectList? Books { get; set; }

        public IList<Book> books { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync()
        {
            if (_context.Book != null)
            {
                IQueryable<string> booksQuery = from s in _context.Book
                                                orderby s.Title
                                                select s.Title;
                Books = new SelectList(await booksQuery.Distinct().ToListAsync());
            }
            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Customer == null || Customer == null)
            {
                return Page();
            }
            var finalList = _context.Book.Where(x => x.Title.Contains(BookName));


            books = await finalList.ToListAsync();

            Customer.IssuedBook = books[0];

            books[0].Issued = true;
            _context.Customer.Add(Customer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
