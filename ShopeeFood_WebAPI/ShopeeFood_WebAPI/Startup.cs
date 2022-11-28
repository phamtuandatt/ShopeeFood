using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Abstractions;
using Microsoft.IdentityModel.Tokens;
using OA_Data.Entities;
using OA_Repository;
using OA_Repository.IRepository;
using OA_Repository.Repository;
using OA_Service.IServices;
using OA_Service.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ShopeeFood_WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddMvc();

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("OA_Data")));

            services.AddScoped<ICityRepository, CityRepository>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddTransient<IBusinessTypeService, BusinessTypeService>();

            services.AddTransient<ICustomerService, CustomerService>();

            services.AddTransient<IEmployeeService, EmployeeService>();

            services.AddTransient<IProductService, ProductService>();

            services.AddTransient<IProductTypeService, ProductTypeService>();

            services.AddTransient<IShopService, ShopService>();

            services.AddTransient<IShopMenuService, ShopMenuService>();

            services.AddTransient<IDeliveryService, DeliveryService>();

            services.AddTransient<ICityDetailService, CityDetailService>();

            services.AddTransient<IOrderService, OrderService>();

            services.AddTransient<IOrderDetailService, OrderDetailService>();

            services.AddTransient<ICityDistrictService, CityDistrictService>();

            services.AddTransient<ICustomerAddressService, CustomerAddressService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
