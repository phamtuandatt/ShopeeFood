using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OA_Data.Entities
{
    public class Product
    {
        public Product()
        {
            ShopMenus = new HashSet<ShopMenu>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
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

        // Foreign key
        [ForeignKey("ProductTypeId")]
        public ProductType ProductType { get; set; }

        // References 
        public ICollection<ShopMenu> ShopMenus { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
