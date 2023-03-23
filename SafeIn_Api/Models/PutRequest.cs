using System.ComponentModel.DataAnnotations;

namespace SafeIn_Api.Models
{
    public class PutRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string UserName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 8)]
        public string CurrentPassword { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 8)]
        public string Password { get; set; }
    }
}
