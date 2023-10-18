using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafeIn_Api.Models
{
    public class Door
    {
        [Key]
        public string Id { get; set; }


        public string DepartmentId { get; set; }

        public Department Department { get; set; }


    }
}
