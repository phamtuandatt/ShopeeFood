using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OA_Data.Entities;
using OA_Service.IServices;
using System.Collections.Generic;

namespace ShopeeFood_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService orderDetailService;

        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            this.orderDetailService = orderDetailService;
        }

        [HttpGet]
        public IEnumerable<OrderDetail> GetOrderDetails()
        {
            return orderDetailService.GetAll();
        }
    }
}
