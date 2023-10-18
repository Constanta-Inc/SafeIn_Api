using Microsoft.Extensions.Hosting;

namespace SafeIn_Api.Models
{
    public class Company
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }

        public List<Department> Departments { get; set; }

    }
}
