using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using OA_Data.Entities;
using OA_Service.IServices;
using ShopeeFood_WebAPI.Models;
using System.Collections.Generic;

namespace ShopeeFood_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityDetailController : ControllerBase
    {
        private readonly ICityDetailService cityDetailService;
        private readonly IBusinessTypeService businessTypeService;
        private readonly IShopService shopService;

        public CityDetailController(ICityDetailService cityDetailService, IBusinessTypeService businessTypeService, IShopService shopService)
        {
            this.cityDetailService = cityDetailService;
            this.businessTypeService = businessTypeService;
            this.shopService = shopService;
        }

        [HttpGet("GetCityDetails")]
        public IEnumerable<CityDetail> GetCityDetails()
        {
            return cityDetailService.GetAll();
        }

        [HttpGet("GetBusinessTypes")]
        public IEnumerable<BusinessType> GetBusinessTypes()
        {
            return businessTypeService.GetAll();
        }

        [HttpGet("GetAll")]
        public IEnumerable<CityDetailModel> GetAll()
        {
            List<CityDetailModel> lst = new List<CityDetailModel>();
            var lst_CityDetails = GetCityDetails();
            foreach (var item in lst_CityDetails)
            {
                var business = businessTypeService.Get(item.BusinessId);
                CityDetailModel model = new CityDetailModel();
                model.CityId = item.CityId;
                model.BusinesId = item.BusinessId;
                model.BusinessName = business.BusinessName;

                lst.Add(model);
            }

            return lst;
        }

    }
}
