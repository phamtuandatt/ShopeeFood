using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OA_Data.Entities;
using OA_Service.IServices;
using System.Collections.Generic;

namespace ShopeeFood_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessTypeController : ControllerBase
    {
        private IBusinessTypeService businessTypeService;

        public BusinessTypeController(IBusinessTypeService businessTypeService)
        {
            this.businessTypeService = businessTypeService;
        }

        [HttpGet]
        public IEnumerable<BusinessType> GetBusinessTypes()
        {
            return businessTypeService.GetAll();
        }
    }
}
