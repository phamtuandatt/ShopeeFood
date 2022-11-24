﻿using Microsoft.AspNetCore.Authorization;
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

        [Route("GetCusByEmail")]
        [HttpGet("GetCusByEmail")]
        public Customer GetCusByEmail(string email)
        {
            return customerService.GetCusByEmail(email);
        }

        //string name, string address, string sex, string email, string phone, string password
        [Route("AddCustomer")]
        [HttpPost("AddCustomer")]
        public bool AddCustomer(Customer customer)
        {
            try
            {
                Customer cus = new Customer();
                cus.CustomerName = customer.CustomerName;
                cus.CustomerAddress = customer.CustomerAddress;
                cus.Sex = customer.Sex;
                cus.Email = customer.Email;
                cus.Phone = customer.Phone;
                cus.Password = customer.Password;

                customerService.Insert(cus);
                customerService.SaveChange();

                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        [Route("CheckExistedCustomer")]
        [HttpGet("CheckExistedCustomer")]
        public Customer CheckExistedCustomer(string email)
        {
            Customer cus = customerService.CheckExisted_Customer(email);
            
            return cus;
        }

        
    }
}
