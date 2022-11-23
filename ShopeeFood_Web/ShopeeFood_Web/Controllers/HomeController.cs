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

namespace ShopeeFood_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public const string SessionKeyName = "_Name";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult HomePageShopeeFood(int cityId, int bussinessId)
        {
            HttpContext.Session.SetString("CityId", cityId + "");
            HttpContext.Session.SetString("BusinessId", bussinessId + "");

            return View();
        }

        public IActionResult Check_Login()
        {
            ViewBag.UserName = HttpContext.Session.GetString("username");
            ViewBag.Image = HttpContext.Session.GetString("image");

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
                // Get link of API
                client.BaseAddress = new Uri("https://localhost:5001/");

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // Access API
                HttpResponseMessage response = await client.GetAsync("api/CityDetail/GetAll");

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
    }
}
