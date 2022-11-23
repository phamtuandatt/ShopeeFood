using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OA_Data.Entities;
using OA_Repository.IRepository;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ShopeeFood_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        // Call Service for CRUD
        private readonly ICityRepository cityRepository;

        public CityController(ICityRepository cityService)
        {
            this.cityRepository = cityService;
        }

        [HttpGet("GetCities")]
        public IEnumerable<City> GetCities()
        {
            return cityRepository.GetCities();
        }
    }
}
