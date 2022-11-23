using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using OA_Data.Entities;
using OA_Service.IServices;
using System.Collections.Generic;

namespace ShopeeFood_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityDistrictController : ControllerBase
    {
        private readonly ICityDistrictService cityDistrictService;

        public CityDistrictController(ICityDistrictService cityDistrictService)
        {
            this.cityDistrictService = cityDistrictService;
        }

        [HttpGet("GetAlls")]
        public IEnumerable<CityDistrict> GetAlls()
        {
            return cityDistrictService.GetAll();            
        }

        [HttpGet("GetDistrictByCity")]
        public IEnumerable<CityDistrict> GetDistrictByCity(int cityId)
        {
            List<CityDistrict> lst_District = new List<CityDistrict>();
            var lst = GetAlls();
            foreach (var item in lst)
            {
                if (item.CityId == cityId)
                {
                    lst_District.Add(item);
                }
            }
            return lst_District;
        }
    }
}
