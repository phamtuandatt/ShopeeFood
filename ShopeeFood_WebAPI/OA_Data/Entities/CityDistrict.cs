using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Data.Entities
{
    public  class CityDistrict
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CityDistrictId { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string CityDistricName { get; set; }

        [Required]
        public int CityId { get; set; }

        //Foreign Key

        [ForeignKey("CityId")]
        public City City { get; set; }
    }
}
