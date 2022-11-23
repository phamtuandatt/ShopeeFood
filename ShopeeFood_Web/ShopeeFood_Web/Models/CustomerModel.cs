using System.ComponentModel.DataAnnotations;

namespace ShopeeFood_Web.Models
{
    public class CustomerModel
    {
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
    }
}
