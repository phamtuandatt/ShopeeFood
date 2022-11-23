using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopeeFood_WebAPI.Models
{
    public class CityDetailModel
    {
        public int CityId { get; set; }
        public int BusinesId { get; set; }
        public string BusinessName { get; set; }
    }
}
