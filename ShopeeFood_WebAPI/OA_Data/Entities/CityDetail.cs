using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OA_Data.Entities
{
    public class CityDetail
    {
        public int CityId { get; set; }

        public int BusinessId { get; set; }

        // Foreign Key - References
        [ForeignKey("CityId")]
        public City City { get; set; }

        [ForeignKey("BusinessId")]
        public BusinessType BusinessType { get; set; }
    }
}
