using Microsoft.AspNetCore.Http;

namespace ShopeeFood_Web.Models
{
    public class FileUpload
    {
        public IFormFile file { get; set; }
        public string Student { get; set; }
    }
}
