using System.ComponentModel.DataAnnotations;

namespace SafeInApiLocal.Models
{
    public class AdminRegisterRequest
    {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [StringLength(50, MinimumLength = 1)]
            public string UserName { get; set; }

            [Required]
            public string CompanyName { get; set; }

            [Required]
            [StringLength(50, MinimumLength = 8)]
            public string Password { get; set; }

            [Compare("Password")]
            public string ConfirmPassword { get; set; }
    }
}
