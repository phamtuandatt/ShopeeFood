using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OA_Data.Entities;
using OA_Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Threading.Tasks;

namespace ShopeeFood_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public TokenController(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Customer customer)
        {
            var user = await GetCustomer(customer.Email, customer.Password);
            if (user != null)
            {
                // Get Key
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                // Create Claim
                var claim = new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", user.CustomerId.ToString()),
                    new Claim("UserName", user.CustomerName),
                    new Claim("Password", user.Password)
                };

                // Setting Token
                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issure"],
                    audience: _configuration["Jwt:Audience"],
                    claim,
                    expires: DateTime.Now.AddMinutes(1),
                    signingCredentials: credentials
                );

                // Encode_Token
                return Ok( new JwtSecurityTokenHandler().WriteToken(token));
            }
            else
            {
                return BadRequest("Invalid creadential");
            }
        }

        [HttpGet]
        public async Task<Customer> GetCustomer(string email, string password)
        {
            return await _context.Customers.FirstOrDefaultAsync(user => user.Email == email && user.Password == password);
        }

    }
}
