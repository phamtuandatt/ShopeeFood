using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Data.Entities
{
    public class Delivery
    {
        public int EmpId { get; set; }

        public int OrderId { get; set; }

        // Foreign Key
        [ForeignKey("EmpId")]
        public Employee Employee { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }
}
