using System;

namespace ShopeeFood_Web.Models
{
    public class RefreshToken
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Expried { get; set; }
    }
}
