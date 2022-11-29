﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopeeFood_Web.Models;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using System.IdentityModel.Tokens.Jwt;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using System.Net;
using Newtonsoft.Json;
using System.Linq;
using RestSharp;
using NuGet.Common;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using NuGet.ProjectModel;
using ShopeeFood_Web.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Encodings.Web;
using System.IO;

namespace ShopeeFood_Web.Controllers
{
    public class CustomerController : Controller
    {
        private IConfiguration _config;
        string baseUrl = "https://localhost:5001/api/Customer/GetCustomers";
        readonly SendMailService sendMailService;

        public CustomerController(IConfiguration configuration, SendMailService sendMailService)
        {
            _config = configuration;
            this.sendMailService = sendMailService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(CustomerModel customer)
        {
            // Call ShopeeFood_WebAPI
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(customer),
                    Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:5001/api/Token/", content))
                {
                    string token = await response.Content.ReadAsStringAsync();
                    if (token == "Invalid credentials")
                    {
                        ViewBag.Message = "Incorrect Email or Password";
                        return RedirectToAction("Login");
                    }

                    HttpContext.Session.SetString("JWToken", token);
                }

                // Get Customer
                var cus = await GetCustomer(customer.Email, customer.Password);
                if (cus != null)
                {
                    HttpContext.Session.SetString("username", cus.CustomerName);
                    HttpContext.Session.SetString("image", cus.Avata + "");
                    HttpContext.Session.SetString("customerId", cus.CustomerId + "");
                    HttpContext.Session.SetString("email", cus.Email);
                    HttpContext.Session.SetString("password", cus.Password);

                    return RedirectToAction("HomePageShopeeFood", "Home");
                }
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            HttpContext.Session.Remove("Status");
            var email = HttpContext.Session.GetString("email");
            var pw = HttpContext.Session.GetString("password");

            var customer = await GetCustomer(email, pw);

            if (customer == null)
            {
                CustomerModel cus = new CustomerModel();
                cus.Email = HttpContext.Session.GetString("email");
                cus.Password = HttpContext.Session.GetString("password");

                var refreshToken = await RefreshToken(cus);
                HttpContext.Session.Remove("JWToken");
                HttpContext.Session.SetString("JWToken", refreshToken);

                var cusNew = await GetCustomer(email, pw);
                

                return View(cusNew);
            }
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Profile(CustomerModel customer)
        {
            int cusId = int.Parse(HttpContext.Session.GetString("customerId"));
            var cus = await GetCusById(cusId);
            customer.CustomerAddress = cus.CustomerAddress;
            customer.Phone = cus.Phone;

            // Customer Not Existed
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(customer),
                    Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync($"https://localhost:5001/api/Customer/UpdateCustomer?id={cusId}&", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Login", "Customer");
                    }
                    return RedirectToAction("Register", "Customer");
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> CustomerAccountPartial()
        {
            int id = int.Parse(HttpContext.Session.GetString("customerId"));
            var cus =  await GetCusById(id);

            return PartialView(cus);
        }

        //[HttpGet]
        //public async Task<IActionResult> CustomerAddressPartial()
        //{
        //    int id = int.Parse(HttpContext.Session.GetString("customerId"));
        //    var cus = await GetCusAddressById(id);
        //    return PartialView(cus);
        //}


        // BẮt lỗi không cho người dùng nhập ký tự đặc biệt vào textbox

        [HttpGet]
        public async Task<IActionResult> CustomerAddress()
        {
            int id = int.Parse(HttpContext.Session.GetString("customerId"));
            var cus = await GetCusAddressById(id);
            ViewBag.Status = HttpContext.Session.GetString("Status");
     
            return View(cus);
        }

        [HttpPost]
        [ActionName("DeleteCustomerAddressAsync")]
        public async Task<IActionResult> DeleteCustomerAddressAsync(int CustomerAddressId)
        {
            // GetAssync -> Get status when excute API
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:5001/api/CustomerAddress/DeleteCustomerAddress?CustomerAddressId={CustomerAddressId}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        HttpContext.Session.SetString("Status", "Delete customer address complete !");
                        return RedirectToAction("CustomerAddress", "Customer");
                    }
                    HttpContext.Session.SetString("Status", "Delete unsuccessful !");
                    return RedirectToAction("CustomerAddress", "Customer");
                }
            }
        }

        [HttpPost]
        [ActionName("EditCustomerAddressAsync")]
        public async Task<IActionResult> EditCustomerAddressAsync(CustomerAddressModel customer)
        {
            int id = int.Parse(HttpContext.Session.GetString("customerId"));
            customer.CustomerId = id;

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(customer),
                    Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:5001/api/CustomerAddress/UpdateCustomerAddress/", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        HttpContext.Session.SetString("Status", "Update address successful !");
                        return RedirectToAction("CustomerAddress", "Customer");
                    }
                    HttpContext.Session.SetString("Status", "Update unsuccessful !");
                    return RedirectToAction("CustomerAddress", "Customer");
                }
            }
        }

        [HttpPost]
        [ActionName("AddCustomerAddressAsync")]
        public async Task<IActionResult> AddCustomerAddressAsync(CustomerAddressModel customer) 
        {
            int id = int.Parse(HttpContext.Session.GetString("customerId"));
            customer.CustomerId = id;

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(customer),
                    Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:5001/api/CustomerAddress/AddCustomerAddress/", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        HttpContext.Session.SetString("Status", "Add new address successful !");
                        return RedirectToAction("CustomerAddress", "Customer");
                    }
                    HttpContext.Session.SetString("Status", "Create unsuccessful !");
                    return RedirectToAction("CustomerAddress", "Customer");
                }
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.Existed = HttpContext.Session.GetString("Existed");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CustomerModel customer)
        {
            if (await IsCustomerExisted(customer.Email))
            {
                // Customer Existed
                HttpContext.Session.SetString("Existed", "Tài khoản email đã được sử dụng \nHãy sử dụng tài khoản Email khác !");

                return RedirectToAction("Register", "Customer");
            }
            else
            {
                // Customer Not Existed
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(customer),
                        Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync("https://localhost:5001/api/Customer/AddCustomer/", content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Login", "Customer");
                        }
                        return RedirectToAction("Register", "Customer");
                    }
                }
            }
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            // Get Customer by Email
            var cus = await GetCusByEmail(email);
            string token = "";

            // Generate Token and Send Email for user
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(cus),
                    Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:5001/api/Token/", content))
                {
                    token = await response.Content.ReadAsStringAsync();

                    HttpContext.Session.SetString("JWToken_ResetPassword", token);
                }
            }

            // When Click Email, Get Token pass CustomerControllerAPI to Authorize
            var urls = Url.Action(action: "ResetPassword", controller: "Customer", values: null, protocol: Request.Scheme);

            //Fetching Email Body Text from EmailTemplate File.  
            string FilePath = "D:\\Demo\\1_ShopeeFood\\ShopeeFood_Web\\ShopeeFood_Web\\MailTemplates\\MailTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();

            //Repalce [_link_content] = urls   
            MailText = MailText.Replace("[_link_content]", urls);

            MailContent mailContent = new MailContent();
            mailContent.To = "phamtuandat16072001@gmail.com";
            mailContent.Title = "RESET PASSOWRD";
            mailContent.Content = MailText;

            await sendMailService.SendMail(mailContent);

            // If success -> Navigation Action ForgotPassword_Message
            return RedirectToAction("ForgotPassword_Message", "Customer");
        }

        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string email, string password)
        {
            HttpClient client = new HttpClient();
            string json = await client.GetStringAsync($"https://localhost:5001/api/Customer/ResetPassword?email={email}&password={password}");
            var res = JsonConvert.DeserializeObject<CustomerModel>(json);
            if (res != null)
            {
                return RedirectToAction("Login", "Customer");
            }
            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword_Message()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("HomePageShopeeFood", "Home");
        }

        public async Task<string> RefreshToken(CustomerModel customer)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(customer),
                    Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:5001/api/Token/", content))
                {
                    string token = await response.Content.ReadAsStringAsync();

                    return token;
                }
            }
        }

        public async Task<bool> IsCustomerExisted(string email)
        {
            using (HttpClient client = new HttpClient())
            {
                string json = await client.GetStringAsync($"https://localhost:5001/api/Customer/IsCustomerExistedtomer?email={email}");
                var res = JsonConvert.DeserializeObject<CustomerModel>(json);

                if (res != null)
                {
                    return true;
                }
                return false;
            }
        }

        public async Task<CustomerModel> GetCustomer(string email, string password)
        {
            using (var clients = new HttpClient())
            {
                var accessToken = HttpContext.Session.GetString("JWToken");
                clients.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                // Access API
                HttpResponseMessage response = await clients.GetAsync($"https://localhost:5001/api/Customer/GetCustomer_2?email={email}&password={password}");

                if (response.IsSuccessStatusCode)
                {
                    // Read data
                    var details = response.Content.ReadAsAsync<CustomerModel>().Result;

                    return details;
                }
                else if ((int)response.StatusCode == 401)
                {
                    return null;
                }
                return null;
            }
        }

        public async Task<List<CustomerModel>> GetCustomers()
        {
            //var accessToken = HttpContext.Session.GetString("JWToken");
            //var url = baseUrl;
            //HttpClient client = new HttpClient();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            //string json = await client.GetStringAsync(url);
            //var res = JsonConvert.DeserializeObject<List<CustomerModel>>(json).ToList();

            //return res;

            using (var clients = new HttpClient())
            {
                var accessToken = HttpContext.Session.GetString("JWToken");
                var url = baseUrl;
                clients.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                // Access API
                HttpResponseMessage response = await clients.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    // Read data
                    var details = response.Content.ReadAsAsync<List<CustomerModel>>().Result;
                    List<CustomerModel> lst = new List<CustomerModel>();
                    foreach (var item in details)
                    {
                        lst.Add(item);
                    }
                    return lst;
                }
                else if ((int)response.StatusCode == 401)
                {
                    return null;
                }
                return null;
            }
        }

        public async Task<CustomerModel> GetCusByEmail(string email)
        {
            var url = baseUrl;
            HttpClient client = new HttpClient();
            string json = await client.GetStringAsync($"https://localhost:5001/api/Customer/GetCusByEmail?email={email}");
            var res = JsonConvert.DeserializeObject<CustomerModel>(json);

            return res;
        }

        public async Task<CustomerModel> GetCusById(int id)
        {
            var url = baseUrl;
            HttpClient client = new HttpClient();
            string json = await client.GetStringAsync($"https://localhost:5001/api/Customer/GetCustomer_1?id={id}");
            var res = JsonConvert.DeserializeObject<CustomerModel>(json);

            return res;
        }

        public async Task<IEnumerable<CustomerAddressModel>> GetCusAddressById(int id)
        {
            var url = baseUrl;
            HttpClient client = new HttpClient();
            string json = await client.GetStringAsync($"https://localhost:5001/api/CustomerAddress/GetAll");
            var res = JsonConvert.DeserializeObject<IEnumerable<CustomerAddressModel>>(json).ToList();

            var model = res.Where(adr => adr.CustomerId == id).ToList();

            return model;
        }
    }
}
