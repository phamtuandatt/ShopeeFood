using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OA_Data.Entities;
using OA_Service.IServices;
using OA_Service.Services;
using System;
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

        [HttpGet("GetAll")]
        public IEnumerable<CustomerAddress> GetAll()
        {
            return customerAddressService.GetAll(); 
        }

        //string name, string address, string sex, string email, string phone, string password

        [HttpPost("AddCustomerAddress")]
        public bool AddCustomerAddress(CustomerAddress customer)
        {
            try
            {
                CustomerAddress customerAddress = new CustomerAddress();
                customerAddress.CustomerId = customer.CustomerId;
                customerAddress.RemmemberName = customer.RemmemberName;
                customerAddress.Address = customer.Address;
                customerAddress.Phone = customer.Phone;
                customerAddress.Name = customer.Name;

                customerAddressService.Insert(customerAddress);
                customerAddressService.SaveChange();

                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }


        [HttpDelete("DeleteCustomerAddress")]
        public bool DeleteCustomerAddress(int CustomerAddressId)
        {
            try
            {
                customerAddressService.Delete(CustomerAddressId);
                customerAddressService.SaveChange();

                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }


        [HttpPut("UpdateCustomerAddress")]
        public bool UpdateCustomerAddress(CustomerAddress customer)
        {
            customerAddressService.Update(customer);
            try
            {
                customerAddressService.SaveChange();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
    }
}
