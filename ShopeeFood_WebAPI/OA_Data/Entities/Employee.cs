using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Data.Entities
{
    public class Employee
    {
        public Employee()
        {
            Deliveries = new HashSet<Delivery>();
        }
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int EmpId { get; set; }

        public string EmpName { get; set; }

        // References 
        public ICollection<Delivery> Deliveries { get; set; }
    }
}
