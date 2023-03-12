using System.ComponentModel.DataAnnotations;

namespace SafeIn_Api.Models
{
    public class RevokeRequest
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
