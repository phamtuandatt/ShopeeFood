using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OA_Data.Entities;
using OA_Service.IServices;
using System.Collections;
using System.Collections.Generic;

namespace ShopeeFood_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpGet]
        public IEnumerable<Employee> GetEmployees()
        {
            return employeeService.GetAll();
        }
    }
}
