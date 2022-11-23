using System.ComponentModel.DataAnnotations.Schema;

namespace ShopeeFood_Web.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string ProductName { get; set; }

        public double Cost { get; set; }

        public string Discount { get; set; }

        public double Price { get; set; }

        public string ProductImage { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string ProductDescription { get; set; }

        public int ProductTypeId { get; set; }
    }
}
