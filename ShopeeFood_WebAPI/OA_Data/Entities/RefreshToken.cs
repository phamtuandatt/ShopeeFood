using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Data.Entities
{
    public class RefreshToken
    {
        [Key]
        public string TokenId { get; set; }

        public string Token { get; set; }

        public string Refresh { get; set; }

        public string IPAddress { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddThh:mm:ss}")]
        public DateTime Exprired { get; set; }

        public bool IsExprired
        {
            get { return DateTime.UtcNow > Exprired; }
        }

        // Foreign Key
        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
    }
}
