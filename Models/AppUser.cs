using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LibraryStoreApp.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        public int CustomerNo {  get; set; }

        public string? Adress {  get; set; }

        public string? Department {  get; set; }

        public string? Scholl {  get; set; }
    }
}
