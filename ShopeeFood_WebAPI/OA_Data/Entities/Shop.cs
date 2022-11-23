using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OA_Data.Entities
{
    public class Shop
    {
        public Shop()
        {
            ShopMenus = new HashSet<ShopMenu>();
            Orders = new HashSet<Order>();
        }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ShopId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string ShopName { get; set; }

        public string Address { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddThh:mm:ss}")]
        public DateTime OpenTime { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddThh:mm:ss}")]
        public DateTime CloseTime { get; set; }

        public string ShopImage { get; set; }

        public int BusinessId { get; set; }

        public int CityId { get; set; }

        public int CityDistricId { get; set; }


        // Foreign Key
        [ForeignKey("BusinessId")]
        public BusinessType BusinessType { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; }

        [ForeignKey("CityDistricId")]
        public CityDistrict CityDistrict { get; set; }

        // References
        public ICollection<ShopMenu> ShopMenus { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
