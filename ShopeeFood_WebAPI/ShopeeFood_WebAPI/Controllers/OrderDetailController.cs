using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OA_Data.Entities;
using OA_Service.IServices;
using OA_Service.Services;
using ShopeeFood_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.WebSockets;

namespace ShopeeFood_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService orderDetailService;
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly IShopService shopService;

        public OrderDetailController(IOrderDetailService orderDetailService, IOrderService orderService, IProductService productService, IShopService shopService)
        {
            this.orderDetailService = orderDetailService;
            this.orderService = orderService;
            this.productService = productService;
            this.shopService = shopService;
        }

        [HttpGet("Cart")]
        public List<CartModel> Cart(int customerId)
        {
            List<CartModel> carts = new List<CartModel>();
            // Get All Order of Customer
            var order = orderService.GetOrderByCustomer(customerId);
            foreach (var item in order)
            {
                var shop = shopService.Get(item.ShopId);
                var orderDetail = orderDetailService.GetOrderDetails(item.OrderId);

                var cart = new CartModel();
                cart.OrderId = item.OrderId;
                cart.OrderDate = item.OrderDate;
                cart.Address = item.Address;
                cart.Status = item.Status;
                cart.TotalOrderQuantity = orderDetail.Sum(s => s.OrderQuantity);
                cart.ShopId = item.ShopId;
                cart.ShopName = shop.ShopName;
                cart.TotalOrderPrice = orderDetail.Sum(s => s.Total_price);

                carts.Add(cart);
            }

            return carts;
        }

        [HttpPost("AddItemOrder")]
        public bool AddItemOrder(OrderDetail item)
        {
            try
            {
                //int orderId = orderService.GetMaxId();
                //item.OrderId = orderId;
                orderDetailService.Insert(item);
                orderDetailService.SaveChange();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("UpdateItemOrder")]
        public bool UpdateItemOrder(OrderDetail orderDetail)
        {
            try
            {
                orderDetailService.Update(orderDetail);
                orderDetailService.SaveChange();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("DeleteItemOrder")]
        public bool DeleteItemOrder(int OrderId)
        {
            try
            {
                // Get OrderDetail
                var orderDetails = orderDetailService.GetOrderDetails(OrderId);
                foreach (var item in orderDetails)
                {
                    orderDetailService.Remove(item);
                }

                orderDetailService.SaveChange();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("GetOrderDetails")]
        public IEnumerable<OrderDetailsModel> GetOrderDetails(int orderId)
        {
            List<OrderDetailsModel> lst = new List<OrderDetailsModel>();

            // Get OrderDetails
            var orderDetails = orderDetailService.GetOrderDetails(orderId);

            foreach (var item in orderDetails)
            {
                // Get Product
                var product = productService.Get(item.ProductId);

                // Create OrderDetailsModel
                OrderDetailsModel ord = new OrderDetailsModel();
                ord.OrderId = item.OrderId;
                ord.ProductId = item.ProductId;
                ord.ProductName = product.ProductName;
                ord.OrderQuantity = item.OrderQuantity;
                ord.Price = item.Price;
                ord.Total_price = item.Total_price;

                lst.Add(ord);
            }

            return lst;
        }

        [HttpGet("ProductIsExistedInOder")]
        public OrderDetail ProductIsExistedInOder(int productId, int customerId, int shopId)
        {
            var order = orderService.OrderIsExisted(customerId, shopId);

            var orderDetails = orderDetailService.GetOrderDetails(order.OrderId);

            foreach (var item in orderDetails)
            {
                if (item.ProductId == productId)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
