using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OA_Data.Entities
{
    public class ShopMenu
    {
        public int ShopId { get; set; }

        public int ProductId { get; set; }

        public int ProductTypeId { get; set; }

        public int ProductQuantity { get; set; }


        // Foreign Key
        [ForeignKey("ShopId")]
        public Shop Shop { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [ForeignKey("ProductTypeId")]
        public ProductType ProductType { get; set; }

    }
}
