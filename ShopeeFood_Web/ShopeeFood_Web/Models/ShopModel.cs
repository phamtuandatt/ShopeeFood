using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace ShopeeFood_Web.Models
{
    public class ShopModel
    {
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
    }
}
