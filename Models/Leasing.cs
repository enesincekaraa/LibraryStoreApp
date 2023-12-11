using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryStoreApp.Models
{
    public class Leasing
    {
        [Key]
        public int Id {  get; set; }

        [Required]
        public int CustomerId {  get; set; }

        [Required]
        [ForeignKey("BookId")]
        public int BookId {  get; set; }

        [ValidateNever]
        public Book Book { get; set; }
    }
}
