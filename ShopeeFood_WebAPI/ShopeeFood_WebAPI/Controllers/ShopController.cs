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
        [HttpGet("GetShopsCityBussinessType")]
        public IEnumerable<Shop> GetShopsCityBussinessType(int cityId, int bussinessId)
        {
            return shopService.GetShopCityBussinessType(cityId, bussinessId);
        }

        // Get Shop by CityId
        [HttpGet("GetShopByCityId")]
        public IEnumerable<Shop> GetShopByCityId(int cityId)
        {
            return shopService.GetShopByCityId(cityId);
        }

        // Get Shop by CityId_CityDistrictId
        [HttpGet("GetShopByCityIdCityDistrictId")]
        public IEnumerable<Shop> GetShopByCityIdCityDistrictId(int cityId, int cityDistrictId)
        {
            return shopService.GetShopByCityIdCityDistrictId(cityId, cityDistrictId);
        }
    }
}
