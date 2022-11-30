using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OA_Data.Entities;
using OA_Service.IServices;
using OA_Service.Services;
using ShopeeFood_WebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopeeFood_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShopService shopService;

        public ShopController(IShopService shopService)
        {
            this.shopService = shopService;
        }

        [HttpGet("GetShops")]
        public IEnumerable<Shop> GetShops()
        {
            return shopService.GetAll();
        }

        [HttpGet("GetShop")]
        public Shop GetShop(int ShopId)
        {
            return shopService.Get(ShopId);
        }

        // Get Shop when pass CityId and BussinessTypeId
        [HttpGet("GetShops_City_BussinessType")]
        public IEnumerable<Shop> GetShops_City_BussinessType(int cityId, int bussinessId)
        {
            return shopService.GetShop_City_BussinessType(cityId, bussinessId);
        }

        // Get Shop by CityId
        [HttpGet("Get_Shop_By_CityId")]
        public IEnumerable<Shop> Get_Shop_By_CityId(int cityId)
        {
            return shopService.Get_Shop_By_CityId(cityId);
        }

        // Get Shop by CityId_CityDistrictId
        [HttpGet("Get_Shop_By_CityId_CityDistrictId")]
        public IEnumerable<Shop> Get_Shop_By_CityId_CityDistrictId(int cityId, int cityDistrictId)
        {
            return shopService.Get_Shop_By_CityId_CityDistrictId(cityId, cityDistrictId);
        }
    }
}
