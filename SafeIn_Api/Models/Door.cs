using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafeIn_Api.Models
{
    public class Door
    {
        [Key]
        public string DoorId { get; set; }


        //[ForeignKey]
        //public string CompanyId { get; set; }
        public Company Company { get; set; }
        public string CompanyId { get; set; }


    }
}
