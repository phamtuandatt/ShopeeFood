﻿using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> ShopCategory(int businessId)
        {
            HttpContext.Session.Remove("Check_District");
            var cityId = int.Parse(HttpContext.Session.GetString("CityId"));
            if (cityId != 0)
            {
                var lst_CityDistricts = await GetCityDistrict(cityId);
                ViewData["CityDistrict"] = lst_CityDistricts;

                return View();
            }
            else
            {
                var lst_CityDistricts = await GetCityDistrict(1);
                ViewData["CityDistrict"] = lst_CityDistricts;

                return View();
            }
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
                if (cityId != 0)
                {
                    if (busnessId != 0)
                    {
                        List<ShopModel> lst_Shop_District = new List<ShopModel>();

                        var lst_checkeds = lst.Split(" ");

                        var lst_Shop = await GetShopByCity(cityId);

                        foreach (var item in lst_Shop)
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
                        List<ShopModel> lst_Shop_District = new List<ShopModel>();

                        var lst_checkeds = lst.Split(" ");

                        var lst_Shop = await GetShopByCity(cityId);

                        foreach (var item in lst_Shop)
                        {
                            var itemMatched = lst_checkeds.FirstOrDefault(t => t == item.CityDistricId.ToString());
                            if (!string.IsNullOrEmpty(itemMatched))
                            {
                                lst_Shop_District.Add(item);
                            }
                        }
                        return PartialView(lst_Shop_District);
                    }
                }
                else
                {
                    if (busnessId != 0)
                    {
                        List<ShopModel> lst_Shop_District = new List<ShopModel>();

                        var lst_checkeds = lst.Split(" ");

                        var lst_Shop = await GetShopByCity(1);

                        foreach (var item in lst_Shop)
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
                        List<ShopModel> lst_Shop_District = new List<ShopModel>();

                        var lst_checkeds = lst.Split(" ");

                        var lst_Shop = await GetShopByCity(1);

                        foreach (var item in lst_Shop)
                        {
                            var itemMatched = lst_checkeds.FirstOrDefault(t => t == item.CityDistricId.ToString());
                            if (!string.IsNullOrEmpty(itemMatched))
                            {
                                lst_Shop_District.Add(item);
                            }
                        }
                        return PartialView(lst_Shop_District);
                    }
                }
            }
            else
            {
                if (cityId != 0)
                {
                    if (busnessId != 0)
                    {
                        var lst_Shop = await GetShopByCity(cityId);

                        return PartialView(lst_Shop);
                    }
                    else
                    {
                        var lst_Shop = await GetShopByCity(cityId);

                        return PartialView(lst_Shop);
                    }
                }
                else
                {
                    if (busnessId != 0)
                    {
                        var lst_Shop = await GetShopByCity(1);

                        return PartialView(lst_Shop);
                    }
                    else
                    {
                        var lst_Shop = await GetShopByCity(1);

                        return PartialView(lst_Shop);
                    }
                }

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
                var model = await GetShops(1, 1);

                return PartialView(model);
            }
            else
            {
                if (cityId == "0" && businessId != "0")
                {
                    var models = await GetShops(1, int.Parse(businessId));

                    return PartialView(models);
                }
                if (cityId != "0" && businessId == "0")
                {
                    var models = await GetShops(int.Parse(cityId), 1);

                    return PartialView(models);
                }
                var model = await GetShops(int.Parse(cityId), int.Parse(businessId));

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

                // Tạo partial View hiển thị danh sách cửa hàng lstShop
                // Khi thực hiện chọn khu vực thì chỉ tìm kiếm trong lstShop 

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
        public async Task<IEnumerable<ShopModel>> GetShops(int cityId, int bussinessId)
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
        public async Task<IEnumerable<ShopModel>> GetShopByCity(int cityId)
        {
            HttpClient client = new HttpClient();
            string json =  await client.GetStringAsync ($"{_baseUrl}Shop/GetShopByCityId?cityId={cityId}");
            var res = JsonConvert.DeserializeObject<List<ShopModel>>(json).ToList();

            return res;
        }

        // Get Shop By DistrictId
        public async Task<IEnumerable<ShopModel>> GetShopByCityDistrict(int cityId, string[] check_district)
        {
            var lst_Shop = await GetShopByCity(cityId);
            List<ShopModel> lst_Shop_District = new List<ShopModel>();
            foreach (var item in lst_Shop)
            {
                for (int i = 0; i < check_district.Length; i++)
                {
                    if (item.CityDistricId == int.Parse(check_district[i]))
                    {
                        lst_Shop_District.Add(item);
                    }
                }
            }

            return lst_Shop_District;
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
            if (cityId == 0)
            {
                cityId = 1;
            }        

            var res = await GetShopByCity(cityId);

            var lst_CityDistricts = await GetCityDistrict(cityId);
            ViewData["CityDistrict"] = lst_CityDistricts;

            var list = res.Where(name => name.ShopName.ToUpper().Contains(con.ToUpper())).ToList();

            return list;
        }


    }
}
