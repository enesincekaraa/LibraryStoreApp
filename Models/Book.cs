using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryStoreApp.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string BookName { get; set; }

        public string Description {  get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [Range(0,5000)]
        public double Price { get; set; }

        [ForeignKey("BookTypeId")]
        [ValidateNever]
        public int BookTypeId {  get; set; }

        [ValidateNever]
        public BookType BookType { get; set; }

        [ValidateNever]
        public string ImageUrl  { get; set; }
    }
}
