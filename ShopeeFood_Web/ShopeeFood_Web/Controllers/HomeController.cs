using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopeeFood_Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks; 
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ShopeeFood_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration  _configuration;
        public const string SessionKeyName = "_Name";
        public string _baseUrl;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _baseUrl = _configuration["CallAPI:BaseURL"];
        }

        public IActionResult HomePageShopeeFood(int cityId, int bussinessId)
        {
            HttpContext.Session.SetString("CityId", cityId + "");
            HttpContext.Session.SetString("BusinessId", bussinessId + "");

            return View();
        }

        public IActionResult CheckLogin()
        {
            ViewBag.UserName = HttpContext.Session.GetString("username");
            ViewBag.Image = HttpContext.Session.GetString("image");
            ViewBag.CustomerId = HttpContext.Session.GetString("customerId");

            return PartialView();
        }

        public async Task<IActionResult> Header(int cityId)
        {
            if (cityId == 0)
            {
                cityId = 1;
            }
            // Get BussinessType
            using (var client = new HttpClient())
            {
                // Access API
                HttpResponseMessage response = await client.GetAsync(_baseUrl + "CityDetail/GetAll");

                if (response.IsSuccessStatusCode)
                {
                    // Read data
                    var details = response.Content.ReadAsAsync<IEnumerable<CityDetail>>().Result;

                    List<CityDetail> cityDetails = new List<CityDetail>();
                    foreach (var item in details)
                    {
                        if (item.CityId == cityId)
                        {
                            cityDetails.Add(item);
                        }
                    }

                    return PartialView(cityDetails);
                }
                else
                {
                    return NotFound("No found");
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> SearchPartialView(string condition)
        {
            var shops = await SearchShopNameAsync(condition);
            return PartialView(shops);
        }

        public async Task<IEnumerable<ShopModel>> SearchShopNameAsync(string condition)
        {
            if (condition != null)
            {
                HttpContext.Session.Remove("Condition");
                HttpContext.Session.SetString("Condition", condition);
            }
            string con = HttpContext.Session.GetString("Condition");
            int cityId = int.Parse(HttpContext.Session.GetString("CityId"));
            if (cityId == 0)
            {
                cityId = 1;
            }
            var res = await GetShopByCity(cityId);

            var list = res.Where(name => name.ShopName.ToUpper().Contains(con.ToUpper())).ToList();

            return list;
        }

        public async Task<IEnumerable<ShopModel>> GetShopByCity(int cityId)
        {
            HttpClient client = new HttpClient();
            string json = await client.GetStringAsync($"{_baseUrl}Shop/GetShopByCityId?cityId={cityId}");
            var res = JsonConvert.DeserializeObject<List<ShopModel>>(json).ToList();

            return res;
        }
    }
}
