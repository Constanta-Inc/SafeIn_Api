namespace SafeIn_Api.Models
{
    public class Department
    {
        public string Id { get; set; }

        public string CompanyId { get; set; }

        public Company Company { get; set; }

        public List<Door> Doors { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
