using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Data.Entities
{
    public class CustomerAddress
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CustomerAddressId { get; set; }

        public int CustomerId { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string RemmemberName { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string Address { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string Phone { get; set; }
    }
}
