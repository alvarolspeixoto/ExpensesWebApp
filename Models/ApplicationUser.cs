using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpensesWebApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string? FullName { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

    }
}
