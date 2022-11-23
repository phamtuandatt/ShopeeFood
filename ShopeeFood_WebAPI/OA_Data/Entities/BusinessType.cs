using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OA_Data.Entities
{
    public class BusinessType
    {
        public BusinessType()
        {
            CityDetails = new HashSet<CityDetail>();
            Shops = new HashSet<Shop>();
        }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int BusinesId { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string BusinessName { get; set; }

        public ICollection<CityDetail> CityDetails { get; set; }
        public ICollection<Shop> Shops { get; set; }
    }
}
