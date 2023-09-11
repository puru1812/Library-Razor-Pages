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
        public async Task<IActionResult> OnGetAsync()
        {
            
            return Page();
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

     

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD

        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Book == null || Book == null)
            {
                return Page();
            }


            _context.Book.Add(Book);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
