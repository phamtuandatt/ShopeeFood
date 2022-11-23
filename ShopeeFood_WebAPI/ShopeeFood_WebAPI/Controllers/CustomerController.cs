using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OA_Data.Entities;
using OA_Service.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopeeFood_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [Route("GetCustomers")]
        [HttpGet("GetCustomers")]
        public IEnumerable<Customer> GetCustomers()
        {
            return customerService.GetAll();
        }

        [Route("GetCustomer_1")]
        [HttpGet("GetCustomer_1")]
        public Customer GetCustomer_1(int id)
        {
            return customerService.Get(id);
        }

        [Route("GetCustomer_2")]
        [HttpGet("GetCustomer_2")]
        [Authorize]
        public Customer GetCustomer_2(string email, string password)
        {
            return customerService.CheckLogin(email, password);
        }

        [Route("AddCustomer")]
        [HttpPost("AddCustomer")]
        public bool AddCustomer(string name, string address, string sex, string email, string phone, string password)
        {
            try
            {
                Customer customer = new Customer();
                customer.CustomerName = name;
                customer.CustomerAddress = address;
                customer.Sex = sex;
                customer.Email = email;
                customer.Phone = phone;
                customer.Password = password;


                customerService.Insert(customer);
                customerService.SaveChange();

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
