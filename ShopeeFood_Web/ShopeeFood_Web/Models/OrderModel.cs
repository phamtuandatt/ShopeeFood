using System.ComponentModel.DataAnnotations;
using System;

namespace ShopeeFood_Web.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddThh:mm:ss}")]
        public DateTime OrderDate { get; set; }

        public string Address { get; set; }

        public int Status { get; set; }

        public double Total_Order { get; set; }

        public int CustomerId { get; set; }

        public int ShopId { get; set; }
    }
}
