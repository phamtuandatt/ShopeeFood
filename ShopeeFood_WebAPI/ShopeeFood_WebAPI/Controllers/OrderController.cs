using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OA_Data.Entities;
using OA_Service.IServices;
using System;
using System.Collections.Generic;

namespace ShopeeFood_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet("GetOrders")]
        public IEnumerable<Order> GetOrders()
        {
            return orderService.GetAll();
        }

        [HttpGet("GetOrder")]
        public Order GetOrder(int orderId)
        {
            return orderService.Get(orderId);
        }

        [HttpPost("AddOrder")]
        public bool AddOrder(Order item)
        {
            try
            {
                orderService.Insert(item);
                orderService.SaveChange();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("UpdateOrder")]
        public bool UpdateOrder(Order item)
        {
            try
            {
                orderService.Update(item);
                orderService.SaveChange();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
        }


        [HttpDelete("DeleteOrder")]
        public bool DeleteOrder(int orderId)
        {
            try
            {
                orderService.Delete(orderId);
                orderService.SaveChange();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("OrderIsExisted")]
        public Order OrderIsExisted(int customerId, int shopId)
        {
            var model = orderService.OrderIsExisted(customerId, shopId);
            
            return model;
        }

        // Get OrderId MAX of CustomerId and ShopId and Status = 0; 
        [HttpGet]
        public int GetMaxId()
        {
            return orderService.GetMaxId();
        }

    }
}
