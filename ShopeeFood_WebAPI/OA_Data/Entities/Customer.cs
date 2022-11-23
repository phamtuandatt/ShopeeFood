using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Data.Entities
{
    public class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
            RefreshTokens = new HashSet<RefreshToken>();
        }
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerAddress { get; set; }

        public string Sex { get; set; }


        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [RegularExpression(@"^((0(\d){9})|(086(\d){7})|(088(\d){7})|(089(\d){7})|(01(\d){9}))$",
            ErrorMessage = "...")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "...")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Avata { get; set; }

        // References 
        public ICollection<Order> Orders { get; set; }

        public ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
