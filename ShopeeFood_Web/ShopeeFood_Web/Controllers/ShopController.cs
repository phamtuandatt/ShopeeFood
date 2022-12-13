using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NuGet.Protocol;
using ShopeeFood_Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Threading.Tasks;

namespace ShopeeFood_Web.Controllers
{
    public class ShopController : Controller
    {
        private readonly IConfiguration _configuration;
        private string _baseUrl;

        public ShopController(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseUrl = _configuration["CallAPI:BaseURL"];
        }


        // ------------------------------------------------------------------------------------
        // --------------- VIEW ---------------------------------------------------------------
        // Show ShopCategory
        [HttpGet]
        public async Task<IActionResult> ShopCategory(int businessId, int cityid)
        {
            HttpContext.Session.Remove("Check_District");
            if (cityid != 0)
            {
                var lst_CityDistricts = await GetCityDistrict(cityid);
                ViewData["CityDistrict"] = lst_CityDistricts;
                HttpContext.Session.SetString("CityId", cityid + "");
                HttpContext.Session.SetString("BusinessId", businessId + "");
                ViewBag.bId = businessId;
                ViewBag.cId = cityid;
                return View();
            }
            else
            {
                var lst_CityDistricts = await GetCityDistrict(1);
                ViewData["CityDistrict"] = lst_CityDistricts;

                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> ShopCategoryHomeCity(int cityId, int bussinessId)
        {
            if (cityId != 0 && bussinessId != 0)
            {
                HttpContext.Session.SetString("CityId", cityId + "");
                HttpContext.Session.SetString("business", bussinessId + "");
            }
            else
            {
                if (cityId == 0 && bussinessId == 0)
                {
                    if (HttpContext.Session.GetString("CityId") == null
                        && HttpContext.Session.GetString("business") == null)
                    {
                        var shops1 = await GetShopsAsync(1, 1);

                        return PartialView(shops1);
                    }
                    else if (HttpContext.Session.GetString("CityId") != null
                        && HttpContext.Session.GetString("business") != null)
                    {
                        var busId = int.Parse(HttpContext.Session.GetString("business"));
                        var cId = int.Parse(HttpContext.Session.GetString("CityId"));
         
                        ViewBag.cityId = cId;
                        ViewBag.busId = busId;

                        var shopHomes = await GetShopsAsync(cId, busId);

                        return PartialView(shopHomes);
                    }
                    else
                    {
                        var cId = int.Parse(HttpContext.Session.GetString("CityId"));
                        ViewBag.cityId = cId;
                        ViewBag.busId = 1;
                        var shopHomes = await GetShopsAsync(cId, 1);

                        return PartialView(shopHomes);
                    }
                }
            }
            
            var idCity = int.Parse(HttpContext.Session.GetString("CityId"));
            var idBus = int.Parse(HttpContext.Session.GetString("business"));
            ViewBag.cityId = idCity;
            ViewBag.busId = idBus;

            var shops = await GetShopsAsync(idCity, idBus);

            return PartialView(shops);
        }

        public async Task<IActionResult> PartialViewShopCategory(string[] check_district)
        {
            var busnessId = int.Parse(HttpContext.Session.GetString("BusinessId"));
            var cityId = int.Parse(HttpContext.Session.GetString("CityId"));

            var lst_checked = "";
            for (int i = 0; i < check_district.Length; i++)
            {
                lst_checked += check_district[i] + " ";
            }
            if (lst_checked.Count() > 0)
            {
                HttpContext.Session.SetString("Check_District", lst_checked.Trim());
            }

            var lst = HttpContext.Session.GetString("Check_District");
            if (lst != null && lst.Length > 0)
            {
                // Check case businessId == 0
                if (busnessId == 0)
                {
                    List<ShopModel> lstShopDistrictAll = new List<ShopModel>();

                    var lstCheckAll = lst.Split(" ");

                    var lstShopAll = await GetShopByCityAsync(cityId);

                    foreach (var item in lstShopAll)
                    {
                        var itemMatched = lstCheckAll.FirstOrDefault(t => t == item.CityDistricId.ToString());
                        if (!string.IsNullOrEmpty(itemMatched))
                        {
                            lstShopDistrictAll.Add(item);
                        }
                    }
                    return PartialView(lstShopDistrictAll);
                }

                // Get list shop in city with business
                List<ShopModel> shopDistrictsBus = new List<ShopModel>();

                var lst_checkeds = lst.Split(" ");

                // Get Shop by CityId, BusinessId
                var lstShopBus = await GetShopByCityBusAsync(cityId, busnessId);

                foreach (var item in lstShopBus)
                {
                    var itemMatched = lst_checkeds.FirstOrDefault(t => t == item.CityDistricId.ToString());
                    if (!string.IsNullOrEmpty(itemMatched))
                    {
                        shopDistrictsBus.Add(item);
                    }
                }
                return PartialView(shopDistrictsBus);
            }
            else
            {
                // Get Shop by CityId, BusinessId
                if (busnessId == 0)
                {
                    var lstShopCity = await GetShopByCityAsync(cityId);

                    return PartialView(lstShopCity);
                }
                var lstShopCityBus = await GetShopByCityBusAsync(cityId, busnessId);

                return PartialView(lstShopCityBus);
            }
        }

        // Get Information Shop
        [HttpGet]
        public async Task<IActionResult> ShopDetails(int ShopId)
        {
            if (ShopId == 0)
            {
                ShopId = int.Parse(HttpContext.Session.GetString("ShopId"));
            }
            else
            {
                HttpContext.Session.SetString("ShopId", ShopId + "");
            }
            using (HttpClient client = new HttpClient())
            {
                string json = await client.GetStringAsync($"{_baseUrl}Shop/GetShop?ShopId={ShopId}");
                var res = JsonConvert.DeserializeObject<ShopModel>(json);

                ViewData["InfoShop"] = new ShopModel()
                {
                    ShopId = res.ShopId,
                    ShopName = res.ShopName,
                    Address = res.Address,
                    OpenTime = res.OpenTime,
                    CloseTime = res.CloseTime,
                    ShopImage = res.ShopImage,
                };

                var model = await GetShopMenu(ShopId);

                ViewBag.Login = HttpContext.Session.GetString("customerId");
                var tt = HttpContext.Session.GetString("ResetCart");
                if (tt != null)
                {
                    if (tt.Equals("Delete")) 
                    {
                        ViewBag.ResetCart = HttpContext.Session.GetString("ResetCart"); 
                    }
                    else
                    {
                        HttpContext.Session.Remove("ResetCart"); 
                    }
                }
                

                return View(model);
            }
        }

        // Show Shop in CityId = 1
        public async Task<IActionResult> ShopCategoryHomeByCity()
        {
            var cityId = HttpContext.Session.GetString("CityId");
            var businessId = HttpContext.Session.GetString("BusinessId");

            if (cityId == "0" && businessId == "0")
            {
                var model = await GetShopsAsync(1, 1);

                return PartialView(model);
            }
            else
            {
                if (cityId == "0" && businessId != "0")
                {
                    var models = await GetShopsAsync(1, int.Parse(businessId));

                    return PartialView(models);
                }
                if (cityId != "0" && businessId == "0")
                {
                    var models = await GetShopsAsync(int.Parse(cityId), 1);

                    return PartialView(models);
                }
                var model = await GetShopsAsync(int.Parse(cityId), int.Parse(businessId));

                return PartialView(model);
            }
        }

        [HttpGet]
        [ActionName("SearchShops")]
        public async Task<IActionResult> SearchShops(string condition)
        {
            if (condition == null)
            {
                var lstShops = await SearchShopNameAsync(HttpContext.Session.GetString("Condition"));
                HttpContext.Session.Remove("Check_District");
                return View(lstShops);
            }
            else
            {
                var lstShop = await SearchShopNameAsync(condition);

                return View(lstShop);
            }
            
        }

        public async Task<IActionResult> SearchShopPartial(string[] check_district)
        {
            var cityId = int.Parse(HttpContext.Session.GetString("CityId"));

            // Thao tác tương tự như PartialViewShopCategory
            string condition = HttpContext.Session.GetString("Condition");

            var lstShop = await SearchShopNameAsync(condition);
            
            var lst_checked = "";
            for (int i = 0; i < check_district.Length; i++)
            {
                lst_checked += check_district[i] + " ";
            }
            if (lst_checked.Count() > 0)
            {
                HttpContext.Session.SetString("Check_District", lst_checked.Trim());
            }

            var lst = HttpContext.Session.GetString("Check_District");

            if (lst != null && lst.Length > 0)
            {
                List<ShopModel> lst_Shop_District = new List<ShopModel>();

                var lst_checkeds = lst.Split(" ");

                foreach (var item in lstShop)
                {
                    var itemMatched = lst_checkeds.FirstOrDefault(t => t == item.CityDistricId.ToString());
                    if (!string.IsNullOrEmpty(itemMatched))
                    {
                        lst_Shop_District.Add(item);
                    }
                }
                return PartialView(lst_Shop_District);
            }
            else
            {
                return PartialView(lstShop);
            }
        }



        // ------------------------------------------------------------------------------------
        // --------------- FUNCTION -----------------------------------------------------------
        // Get Shop when pass cityId, bussinessId
        public async Task<IEnumerable<ShopModel>> GetShopsAsync(int cityId, int bussinessId)
        {
            HttpClient client = new HttpClient();
            //string json = await client.GetStringAsync($"{_baseUrl}Shop/GetShopByCityIdCityDistrictId?cityId={cityId}&cityDistrictId={bussinessId}");
            string json = await client.GetStringAsync($"{_baseUrl}Shop/GetShopsCityBussinessType?cityId={cityId}&bussinessId={bussinessId}");
            var res = JsonConvert.DeserializeObject<List<ShopModel>>(json).ToList();
            List<ShopModel> shopModels = new List<ShopModel>();
            int count = 0;
            foreach (var item in res)
            {
                if(count < 6)
                {
                    shopModels.Add(item);
                }
                count++;
            }

            return shopModels;
        }

        // Get Shop when pass cityId
        public async Task<IEnumerable<ShopModel>> GetShopByCityAsync(int cityId)
        {
            HttpClient client = new HttpClient();
            string json =  await client.GetStringAsync ($"{_baseUrl}Shop/GetShopByCityId?cityId={cityId}");
            var res = JsonConvert.DeserializeObject<List<ShopModel>>(json).ToList();

            return res;
        }

        // Get Shop By DistrictId
        public async Task<IEnumerable<ShopModel>> GetShopByCityBusAsync(int cityId, int busId)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{_baseUrl}Shop/GetShopsCityBussinessType?cityId={cityId}&bussinessId={busId}");

                if (response.IsSuccessStatusCode)
                {
                    // Read data
                    var details = response.Content.ReadAsAsync<List<ShopModel>>().Result;

                    return details;
                }
            }
            return null;
        }

        // Get ShopMenu
        public async Task<IEnumerable<ShopMenuModel>> GetShopMenu(int ShopId)
        {
            using (HttpClient client = new HttpClient())
            {
                string json = await client.GetStringAsync($"{_baseUrl}ShopMenu/GetShopMenu?shopId={ShopId}");
                var res = JsonConvert.DeserializeObject<List<ShopMenuModel>>(json).ToList();

                return res;
            }
        }

        // Get CityDistrict
        public async Task<IEnumerable<CityDistrictModel>> GetCityDistrict(int cityId)
        {
            using (HttpClient client = new HttpClient())
            {
                string json = await client.GetStringAsync($"{_baseUrl}CityDistrict/GetDistrictByCity?cityId={cityId}");
                var res = JsonConvert.DeserializeObject<List<CityDistrictModel>>(json).ToList();

                return res;
            }
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

            var res = await GetShopByCityAsync(cityId);

            var lst_CityDistricts = await GetCityDistrict(cityId);
            ViewData["CityDistrict"] = lst_CityDistricts;

            var list = res.Where(name => name.ShopName.ToUpper().Contains(con.ToUpper())).ToList();

            return list;
        }


    }
}
