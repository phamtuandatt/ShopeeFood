using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using ShopeeFood_Web.Models;
using System.Linq;

namespace ShopeeFood_Web.Controllers
{
    public class CityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
         

        public async Task<ActionResult> Partial_Nav_menu()
        {
            using (HttpClient client = new HttpClient())
            {
                string json = await client.GetStringAsync("https://localhost:5001/api/City/GetCities");
                var res = JsonConvert.DeserializeObject<List<CityModel>>(json).ToList();

                return PartialView("Partial_Nav_menu", res);
            }
        }

        public async Task<ActionResult> Partial_CityDetial(int cityId)
        {
            if (cityId == 0)
            {
                cityId = 1;
            }
            using (var client = new HttpClient())
            {
                string json = await client.GetStringAsync("https://localhost:5001/api/CityDetail/GetAll");
                var res = JsonConvert.DeserializeObject<List<CityDetail>>(json).ToList();
                List<CityDetail> cityDetails = new List<CityDetail>();
                foreach (var item in res)
                {
                    if (item.CityId == cityId)
                    {
                        cityDetails.Add(item);
                    }
                }

                return PartialView("Partial_CityDetial", cityDetails);
            }
        }

    }
}
