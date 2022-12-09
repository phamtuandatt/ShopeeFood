using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Data.Entities
{
    public class OrderDetail
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int OrderQuantity { get; set; }

        public double Price { get; set; }

        public double Total_price { get; set; }

        //// Foreign Key
        //[ForeignKey("OrderId")]
        //public Order Order { get; set; }

        //[ForeignKey("ProductId")]
        //public Product Product { get; set; }
    }
}
