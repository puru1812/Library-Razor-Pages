using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Library.BooksData;
using Library.Modals;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Library.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly Library.BooksData.LibraryContext _context;

        public IndexModel(Library.BooksData.LibraryContext context)
        {
            _context = context;
        }

        public IList<Book> Book { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? BookGenre { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? BookAuthor { get; set; }

        public SelectList? Genres { get; set; }

        public SelectList? Authors { get; set; }

        public async Task OnGetAsync()
        {

            if (_context.Book != null)
            {
                IQueryable<string> genreQuery = from m in _context.Book
                                                orderby m.Genre
                                                select m.Genre;
                IQueryable<string> authorQuery = from m in _context.Book
                                                orderby m.Author
                                                select m.Author;
                var books = from m in _context.Book
                         select m;
                var finalList = books;
                if (!string.IsNullOrEmpty(SearchString) && !string.IsNullOrEmpty(BookGenre) && !string.IsNullOrEmpty(BookAuthor))
                {

                    finalList = books.Where(x => x.Title.Contains(SearchString) && x.Genre.Contains(BookGenre) && x.Genre.Contains(BookAuthor));

                }
                else if (!string.IsNullOrEmpty(SearchString) && !string.IsNullOrEmpty(BookGenre))
                {

                    finalList = books.Where(x => x.Title.Contains(SearchString) && x.Genre.Contains(BookGenre));

                }
                else if (!string.IsNullOrEmpty(BookAuthor) && !string.IsNullOrEmpty(BookGenre))
                {

                    finalList = books.Where(x => x.Author.Contains(BookAuthor) && x.Genre.Contains(BookGenre));

                }
                if (!string.IsNullOrEmpty(SearchString)&&!string.IsNullOrEmpty(BookAuthor))
                {

                    finalList = books.Where(x => x.Title.Contains(SearchString)&&x.Author.Contains(BookAuthor));

                }
                else if (!string.IsNullOrEmpty(SearchString))
                {

                    
                    finalList = books.Where(x => x.Title.Contains(SearchString));

                }
                else if (!string.IsNullOrEmpty(BookGenre))
                {

                    finalList = finalList.Where(x => x.Genre.Contains(BookGenre));

                }
                else if (!string.IsNullOrEmpty(BookAuthor))
                {

                    finalList = finalList.Where(x => x.Author.Contains(BookAuthor));

                }

                Genres = new SelectList(await genreQuery.Distinct().ToListAsync());


                Authors = new SelectList(await authorQuery.Distinct().ToListAsync());
                //_context.Book.ToListAsync();
                Book = await finalList.ToListAsync();
           }
        }
       
    }
}
