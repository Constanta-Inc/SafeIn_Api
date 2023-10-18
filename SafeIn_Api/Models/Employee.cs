using Microsoft.AspNetCore.Identity;

namespace SafeIn_Api.Models
{
    public class Employee: IdentityUser
    {
        public string? RefreshToken { get; set; }
        public Company Company { get; set; }
        public string CompanyId { get; set; }

        public bool IsInside { get; set; }

        public List<Entrance> Entrances { get; set; } = new();

        public List<Department> Departments { get; set; } = new();
    }
}
