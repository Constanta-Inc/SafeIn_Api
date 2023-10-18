namespace SafeIn_Api.Models
{
    public class Entrance
    {
        public string Id { get; set; }

        public string EmployeeId { get; set; }

        public DateTime DateTime { get; set; }

        public Employee Employee { get; set; }

        public bool Entered { get; set; }
    }
}
