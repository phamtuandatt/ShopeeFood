using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopeeFood_Web.Models;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace ShopeeFood_Web.Controllers
{
    public class ProductController : Controller
    {
        string baseUrl = "https://localhost:5001/api/Product/GetProducts";
        public async Task<IActionResult> ProductCategory()
        {
            var model = await GetProducts();
            return View(model);
        }

        public IActionResult ProductDetail()
        {
            return View();
        }

        [HttpGet]
        public async Task<IEnumerable<ProductModel>> GetProducts()
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            var url = baseUrl;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            string json = await client.GetStringAsync(url);
            var res = JsonConvert.DeserializeObject<List<ProductModel>>(json).ToList();

            return res;
        }
    }
}
