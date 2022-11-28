using System.ComponentModel.DataAnnotations.Schema;

namespace ShopeeFood_Web.Models
{
    public class CustomerAddressModel
    {
        public int CustomerAddressId { get; set; }

        public int CustomerId { get; set; }

        public string RemmemberName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }
    }
}
