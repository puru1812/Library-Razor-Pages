using Microsoft.EntityFrameworkCore;

using Library.BooksData;

namespace Library.Modals
{

    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LibraryContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<LibraryContext>>()))
            {
                if (context == null || context.Book == null)
                {
                    throw new ArgumentNullException("Null LibraryBooksContext");
                }

                // Look for any books.
                if (!context.Book.Any())
                {
                   
                

                context.Book.AddRange(
                    new Book
                    {
                        Title = "Eat, Pray, Love",
                        Genre = "Autobiographies",
                        Author = "Elizabeth Gilbert",
                        Issued = false
                    },

                    new Book
                    {
                        Title = "Atomic Habits",
                        Genre = "Self-Help",
                        Author = "James Clear",
                        Issued = false
                    }

                );
                }
                if (context == null || context.Customer == null)
                {
                    throw new ArgumentNullException("Null LibraryCustomerContext");
                }

                if (!context.Customer.Any())
                {



                    context.Customer.AddRange(
                        new Customer
                        {
                            Name="Cherry",
                            DateOfBirth= DateTime.Now

                        }

                        

                    );
                }
                context.SaveChanges();
            }
        }
    }
}
