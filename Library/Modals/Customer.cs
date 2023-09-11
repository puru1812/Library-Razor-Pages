using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Modals
{
    public class Customer
    {
        public int Id { get; set; }// Primary key

        public string? Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [ForeignKey("IssuedBookId")]
        public Book? IssuedBook { get; set; }
    }
}
