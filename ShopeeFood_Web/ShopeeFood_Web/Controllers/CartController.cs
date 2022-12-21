using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShopeeFood_Web.Models;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using NuGet.Protocol;
using System.Security.Cryptography;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Reflection;
using MailKit.Search;
using Microsoft.CodeAnalysis;

namespace ShopeeFood_Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IConfiguration _configuration;
        private string _baseUrl;

        public CartController(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseUrl = _configuration["CallAPI:BaseURL"];
        }

        [HttpGet]
        public async Task<IActionResult> AddItemCart(int productId, int shopId)
        {
            if (HttpContext.Session.GetString("customerId") == null)
            {
                return RedirectToAction("Login", "Customer");
            }

            var customerId = int.Parse(HttpContext.Session.GetString("customerId"));
            var product = await GetProductAsync(productId);

            // Check Product Is Existed in OrderDetail
            // True -> Update Quantity, Total
            var od = await GetOrderItemAsync(productId, customerId, shopId);
            if (od != null)
            {
                using (var httpClient = new HttpClient())
                {
                    // Get OrderId
                    HttpContext.Session.SetString("OrderId", od.OrderId + "");
                    od.OrderQuantity += 1;
                    od.Total_price = od.OrderQuantity * od.Price;
                    StringContent content = new StringContent(JsonConvert.SerializeObject(od),
                            Encoding.UTF8, "application/json");
                    using (var respones = await httpClient.PutAsync($"{_baseUrl}OrderDetail/UpdateItemOrder/", content))
                    {
                        if (respones.IsSuccessStatusCode)
                        {
                            HttpContext.Session.SetString("ShopId", shopId + "");
                            HttpContext.Session.SetString("ResetCart", "Insert");
                            return RedirectToAction("ShopDetails", "Shop");
                        }
                    }
                }
            }

            // Check ShopId, Customer IsExisted 
            // Get Order of Customer, Shop by status = 0
            var chkShop = await OrderIsExistedAsync(customerId, shopId);
            if (chkShop == null)
            {
                // If false -> Insert Order
                var orders = new OrderModel();
                orders.OrderDate = DateTime.UtcNow.Date;
                orders.Address = "";
                orders.Status = 0;
                orders.Total_Order = 1;
                orders.CustomerId = customerId;
                orders.ShopId = shopId;

                // Insert Order then Insert OrderDetails in  Order
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(orders),
                        Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync($"{_baseUrl}Order/AddOrder/", content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            // Get Order
                            var ord = await OrderIsExistedAsync(customerId, shopId);

                            // Create OrderDetails
                            var orderDetails = new OrderDetailModel();
                            orderDetails.OrderId = ord.OrderId;
                            orderDetails.ProductId = product.ProductId;
                            orderDetails.OrderQuantity = 1;
                            orderDetails.Price = product.Price;
                            orderDetails.Total_price = orderDetails.OrderQuantity * orderDetails.Price;

                            // Get OrderId
                            HttpContext.Session.SetString("OrderId", ord.OrderId + "");

                            StringContent content2 = new StringContent(JsonConvert.SerializeObject(orderDetails),
                                Encoding.UTF8, "application/json");
                            using (var respones2 = await httpClient.PostAsync($"{_baseUrl}OrderDetail/AddItemOrder/", content2))
                            {
                                if (respones2.IsSuccessStatusCode)
                                {
                                    HttpContext.Session.SetString("ShopId", shopId + "");
                                    HttpContext.Session.SetString("ResetCart", "Insert");
                                    return RedirectToAction("ShopDetails", "Shop");
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                //  MAX is Lasted Order of CustomerId and ShopId So
                // If true -> Get OrderId MAX of CustomerId and ShopId and Status = 0; If No have OrderId -> Insert Order
                var orderDetails = new OrderDetailModel();
                orderDetails.OrderId = chkShop.OrderId;
                orderDetails.ProductId = product.ProductId;
                orderDetails.OrderQuantity = 1;
                orderDetails.Price = product.Price;
                orderDetails.Total_price = orderDetails.OrderQuantity * orderDetails.Price;

                // Get OrderId
                HttpContext.Session.SetString("OrderId", orderDetails.OrderId + "");

                // Insert OrderDetails in  Order
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(orderDetails),
                            Encoding.UTF8, "application/json");
                    using (var respones = await httpClient.PostAsync($"{_baseUrl}OrderDetail/AddItemOrder/", content))
                    {
                        if (respones.IsSuccessStatusCode)
                        {
                            HttpContext.Session.SetString("ShopId", shopId + "");
                            HttpContext.Session.SetString("ResetCart", "Insert");
                            return RedirectToAction("ShopDetails", "Shop");
                        }
                    }
                }
            }
            return RedirectToAction("ShopDetails", "Shop");
        }

        public async Task<IActionResult> OrderDetaisPartial()
        {
            // Get customerId
            var customerId = int.Parse(HttpContext.Session.GetString("customerId"));

            // Get shopOd
            var shopId = int.Parse(HttpContext.Session.GetString("ShopId"));

            // Get order
            var order = await OrderIsExistedAsync(customerId, shopId);

            // Get orderDetails
            if (order == null)
            {
                var md = new OrderDetailModel();
                ViewBag.Img = HttpContext.Session.GetString("image");
                return PartialView(md);
            }

            // Get customer
            int cusId = int.Parse(HttpContext.Session.GetString("customerId"));
            var cus = await GetCusByIdAsync(cusId);
            ViewData["InfoCustomer"] = new CustomerModel()
            {
                CustomerName = cus.CustomerName,
                Avata = cus.Avata,
            };

            var model = await GetOrderDetailsAsync(order.OrderId);

            foreach (var item in model)
            {
                ViewBag.OrderId = item.OrderId;
                ViewBag.Img = HttpContext.Session.GetString("image");
                break;
            }

            double totalOrder = model.Sum(t => t.Total_price);

            ViewBag.TotalOrder = string.Format("{0:0,0}", totalOrder);

            ViewBag.OrderId = order.OrderId;

            ViewBag.Img = HttpContext.Session.GetString("image");

            return PartialView(model);
        }

        public async Task<IActionResult> DeleteCart(int orderId)
        {
            // Delete OrderDetails
            using (var httpClient = new HttpClient())
            {
                using (var respones = await httpClient.DeleteAsync($"{_baseUrl}OrderDetail/DeleteItemOrder?OrderId={orderId}"))
                {
                    if (respones.IsSuccessStatusCode)
                    {
                        HttpContext.Session.SetString("ResetCart", "Delete");
                        return RedirectToAction("ShopDetails", "Shop");
                    }
                }
            }
            return RedirectToAction("ShopDetails", "Shop");
        }

        public async Task<IActionResult> UpdateQuantityCart(int productId, int quantity)
        {
            // Get OrderDetail
            var customerId = int.Parse(HttpContext.Session.GetString("customerId"));
            var shopId = int.Parse(HttpContext.Session.GetString("ShopId"));

            // Get OrderDetail want to Update
            var orderDetails = await GetOrderItemAsync(productId, customerId, shopId);
            orderDetails.OrderQuantity = quantity;
            orderDetails.Total_price = quantity * orderDetails.Price;

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(orderDetails),
                        Encoding.UTF8, "application/json");
                using (var respones = await httpClient.PutAsync($"{_baseUrl}OrderDetail/UpdateItemOrder/", content))
                {
                    if (respones.IsSuccessStatusCode)
                    {
                        HttpContext.Session.SetString("ShopId", shopId + "");
                        HttpContext.Session.SetString("ResetCart", "Update");
                        return RedirectToAction("ShopDetails", "Shop");
                    }
                }
            }
            return RedirectToAction("ShopDetails", "Shop");
        }

        [HttpGet]
        public async Task<IActionResult> Pay(int orderId, string address)
        {
            // Get Order
            var order = await GetOrder(orderId);

            // Get OrderDetails Of Order
            var orderDetails = await GetOrderDetailsAsync(order.OrderId);

            // Update Order
            order.Address = address;
            order.Status = 1;
            order.Total_Order = orderDetails.Sum(qt => qt.OrderQuantity);

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(order),
                    Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync($"{_baseUrl}Order/UpdateOrder/", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        HttpContext.Session.SetString("OrderStatus", "Orders successful !");
                        HttpContext.Session.SetString("ResetCart", "Pay");
                        return RedirectToAction("CustomerCart", "Cart");
                    }
                    HttpContext.Session.SetString("OrderStatus", "Orders unsuccessful !");
                    return RedirectToAction("CustomerCart", "Cart");
                }
            }
        }

        public async Task<IActionResult> CustomerCart(int customerId)
        {
            int cusId = int.Parse(HttpContext.Session.GetString("customerId"));
            var cart = await GetCartCusAsync(cusId);

            return View(cart);
        }

        public async Task<IActionResult> CustomerDetailCartPartial(int orderId)
        {
            if (orderId != 0)
            {
                HttpContext.Session.SetString("OrderIdCart", orderId + "");
            }
            int id = int.Parse(HttpContext.Session.GetString("OrderIdCart"));
            
            var orderDetail = await GetOrderDetailsAsync(id);

            return PartialView(orderDetail);
        }

        public async Task<IActionResult> ContentOrderDetailsPartial(int orderId)
        {
            if (orderId != 0)
            {
                HttpContext.Session.SetString("OrderIdCart", orderId + "");
            }

            // Get CustomerAddress
            var customerId = int.Parse(HttpContext.Session.GetString("customerId"));
            var cusAddress = await GetAddressAsync(customerId);
            
            ViewData["CusAdd"] = cusAddress;

            int id = int.Parse(HttpContext.Session.GetString("OrderIdCart"));

            var orderDetail = await GetOrderDetailsAsync(id);

            return PartialView(orderDetail);
        }

        // ------------------------------------------------------------------------------------
        // --------------- FUNCTION -----------------------------------------------------------
        // Get Orders Of Customer
        public async Task<IEnumerable<CustomerCartModel>> GetCartCusAsync(int customerId)
        {
            using (HttpClient client = new HttpClient())
            {
                string json = await client.GetStringAsync($"{_baseUrl}OrderDetail/Cart?customerId={customerId}");
                var res = JsonConvert.DeserializeObject<List<CustomerCartModel>>(json).ToList();

                return res;
            }
        }

        // Function GetProduct
        public async Task<ProductModel> GetProductAsync(int productId)
        {
            using (var clients = new HttpClient())
            {
                // Access API
                HttpResponseMessage response = await clients.GetAsync($"{_baseUrl}Product/GetProduct?productId={productId}");

                if (response.IsSuccessStatusCode)
                {
                    // Read data
                    var model = response.Content.ReadAsAsync<ProductModel>().Result;

                    return model;
                }
                else if ((int)response.StatusCode == 401)
                {
                    return null;
                }
                return null;
            }
        }

        // Get Order is Existed
        public async Task<OrderModel> OrderIsExistedAsync(int customerId, int shopId)
        {
            using (HttpClient client = new HttpClient())
            {
                string json = await client.GetStringAsync($"{_baseUrl}Order/OrderIsExisted?customerId={customerId}&shopId={shopId}");
                var res = JsonConvert.DeserializeObject<OrderModel>(json);

                return res;
            }
        }

        // Get OrderDetails of Order
        public async Task<IEnumerable<OrderDetailModel>> GetOrderDetailsAsync(int orderId)
        {
            using (HttpClient client = new HttpClient())
            {
                string json = await client.GetStringAsync($"{_baseUrl}OrderDetail/GetOrderDetails?orderId={orderId}");
                var res = JsonConvert.DeserializeObject<List<OrderDetailModel>>(json).ToList();

                return res;
            }
        }

        public async Task<CustomerModel> GetCusByIdAsync(int customerId)
        {
            using (var clients = new HttpClient())
            {
                // Access API
                HttpResponseMessage response = await clients.GetAsync($"{_baseUrl}Customer/Cus/{customerId}");

                if (response.IsSuccessStatusCode)
                {
                    // Read data
                    var model = response.Content.ReadAsAsync<CustomerModel>().Result;

                    return model;
                }
                return null;
            }
        }

        public async Task<OrderDetailModel> GetOrderItemAsync(int productId, int customerId, int shopId)
        {
            using (var clients = new HttpClient())
            {
                // Access API
                HttpResponseMessage response = await clients.GetAsync($"{_baseUrl}OrderDetail/ProductIsExistedInOder?productId={productId}&customerId={customerId}&shopId={shopId}");

                if (response.IsSuccessStatusCode)
                {
                    // Read data
                    var model = response.Content.ReadAsAsync<OrderDetailModel>().Result;

                    return model;
                }
                return null;
            }
        }

        public async Task<OrderModel> GetOrder(int orderId)
        {
            //
            using (HttpClient client = new HttpClient())
            {
                string json = await client.GetStringAsync($"{_baseUrl}Order/GetOrder?orderId={orderId}");
                var res = JsonConvert.DeserializeObject<OrderModel>(json);

                return res;
            }
        }

        // Get CustomerAddrss
        public async Task<IEnumerable<CustomerAddressModel>> GetAddressAsync(int customerId)
        {
            using (HttpClient client = new HttpClient())
            {
                string json = await client.GetStringAsync($"{_baseUrl}CustomerAddress/Get?customerId={customerId}");
                var res = JsonConvert.DeserializeObject<List<CustomerAddressModel>>(json).ToList();

                return res;
            }
        }
    }
}
