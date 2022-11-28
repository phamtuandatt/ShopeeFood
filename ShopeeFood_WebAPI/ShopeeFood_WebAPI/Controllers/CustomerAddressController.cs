using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OA_Data.Entities;
using OA_Service.IServices;
using System.Collections.Generic;

namespace ShopeeFood_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAddressController : ControllerBase
    {
        private readonly ICustomerAddressService customerAddressService;

        public CustomerAddressController(ICustomerAddressService customerAddressService)
        {
            this.customerAddressService = customerAddressService;
        }

        [Route("GetAll")]
        [HttpGet("GetAll")]
        public IEnumerable<CustomerAddress> GetAll()
        {
            return customerAddressService.GetAll(); 
        }
    }
}
