using System.ComponentModel.DataAnnotations;
using System;

namespace ShopeeFood_WebAPI.Models
{
    public class CartModel
    {
        public int OrderId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddThh:mm:ss}")]
        public DateTime OrderDate { get; set; }

        public string Address { get; set; }

        public int Status { get; set; }

        public double TotalOrderQuantity { get; set; }

        public int ShopId { get; set; }

        public string ShopName { get; set; }

        public double TotalOrderPrice { get; set; }
    }
}
