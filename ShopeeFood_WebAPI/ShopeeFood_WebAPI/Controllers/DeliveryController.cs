using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OA_Data.Entities;
using OA_Service.IServices;
using System.Collections.Generic;

namespace ShopeeFood_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService deliveryService;

        public DeliveryController(IDeliveryService deliveryService)
        {
            this.deliveryService = deliveryService;
        }

        [HttpGet]
        public IEnumerable<Delivery> GetDeliveries()
        {
            return deliveryService.GetAll();
        }
    }
}
