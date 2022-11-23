using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OA_Data.Entities
{
    public class ProductType
    {
        public ProductType()
        {
            Products = new HashSet<Product>();
            ShopMenus = new HashSet<ShopMenu>();
        }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ProductTypeId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string ProducTypeName { get; set; }

        public ICollection<Product> Products { get; set; }

        public ICollection<ShopMenu> ShopMenus { get; set; }
    }
}
