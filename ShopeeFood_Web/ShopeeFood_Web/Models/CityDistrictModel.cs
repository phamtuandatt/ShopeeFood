using System.ComponentModel.DataAnnotations.Schema;

namespace ShopeeFood_Web.Models
{
    public class CityDistrictModel
    {
        public int CityDistrictId { get; set; }

        public string CityDistricName { get; set; }

        public int CityId { get; set; }
    }
}
