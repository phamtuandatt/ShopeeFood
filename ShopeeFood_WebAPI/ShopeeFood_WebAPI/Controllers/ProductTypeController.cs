using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OA_Data.Entities;
using OA_Service.IServices;
using System.Collections;
using System.Collections.Generic;

namespace ShopeeFood_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeService productTypeService;

        public ProductTypeController(IProductTypeService productTypeService)
        {
            this.productTypeService = productTypeService;
        }

        [HttpGet]
        public IEnumerable<ProductType> GetProductTypes()
        {
            return productTypeService.GetAll();
        }
    }
}
