using Microsoft.Extensions.Hosting;

namespace SafeIn_Api.Models
{
    public class Company
    {
        public string CompanyId { get; set; }
        public string Name { get; set; }
        public List<Door> Doors { get; set; }
        public List<Employee> Employees { get; set; }

    }
}
