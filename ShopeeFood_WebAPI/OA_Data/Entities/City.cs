using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OA_Data.Entities
{
    public class City
    {
        public City()
        {
            CityDetails = new HashSet<CityDetail>();
            Shops = new HashSet<Shop>();
        }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CityId { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string CityName { get; set; }

        public ICollection<CityDetail> CityDetails { get; set; }

        public ICollection<Shop> Shops { get; set; }

        public ICollection<CityDistrict> CityDistricts { get; set; }
    }
}
