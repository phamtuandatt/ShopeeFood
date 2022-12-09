using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Data.Entities
{
    public class Order
    {
        public Order()
        {
            //OrderDetails = new HashSet<OrderDetail>();
        }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int OrderId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddThh:mm:ss}")]
        public DateTime OrderDate { get; set; }

        public string Address { get; set; }

        public int Status { get; set; }

        public double Total_Order { get; set; }

        public int CustomerId { get; set; }

        public int ShopId { get; set; }


        //// Foreign Key
        //[ForeignKey("CustomerId")]
        //public Customer Customer { get; set; }

        //[ForeignKey("ShopId")]
        //public Shop Shop { get; set; }

        // References
        //public ICollection<OrderDetail> OrderDetails { get; set; }


    }
}
