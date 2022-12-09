using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using ShopeeFood_Web.Models;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ShopeeFood_Web.Controllers
{
    public class CityController : Controller
    {
        private IConfiguration _configuration;
        private string baseUrl;

        public CityController(IConfiguration configuration)
        {
            _configuration = configuration;
            baseUrl = _configuration["CallAPI:BaseURL"];
        }

        public IActionResult Index()
        {
            return View();
        }
         

        public async Task<ActionResult> PartialNavmenu()
        {
            using (HttpClient client = new HttpClient())
            {
                string json = await client.GetStringAsync(baseUrl + "City/GetCities");
                var res = JsonConvert.DeserializeObject<List<CityModel>>(json).ToList();

                return PartialView("PartialNavmenu", res);
            }
        }

        public async Task<ActionResult> PartialCityDetial(int cityId)
        {
            if (cityId == 0)
            {
                cityId = 1;
            }
            using (var client = new HttpClient())
            {
                string url = baseUrl + "CityDetail/GetAll";
                string json = await client.GetStringAsync(url);
                var res = JsonConvert.DeserializeObject<List<CityDetail>>(json).ToList();

                var cityDetails = res.Where(ct => ct.CityId == cityId).ToList();

                return PartialView("PartialCityDetial", cityDetails);


            }
        }
    }
}
