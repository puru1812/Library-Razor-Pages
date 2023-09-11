using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Modals
{
    public class Book
    {
        public int Id { get; set; }// Primary key
        public string? Title { get; set; }
        [Display(Name = "Category")]
        public string? Genre { get; set; }
        public string? Author { get; set; }//The question mark after string indicates that the property is nullable

       
        public bool Issued { get; set; }

        public Customer? CustomerId { get; set; }


    }
}
