using Microsoft.AspNetCore.Identity;

namespace SafeIn_Api.Models
{
    public class Employee: IdentityUser
    {
       // public int Id { get; set; }
       // public string LastName { get; set; }
       // public string FirstName { get; set; }
       // public string Email { get; set; }
      //  public string Password { get; set; }

        public string? RefreshToken { get; set; }
        //public string CompanyId { get; set; }
        public Company Company { get; set; }
        public string CompanyId { get; set; }

        //public bool IsInside { get; set; }
    }
}
