using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ShopeeFood_Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly IConfiguration _configuration;
        public static string _baseUrl;
        
        public BaseController(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseUrl = _configuration["CallAPI:BaseURL"];
        }
    }
}
