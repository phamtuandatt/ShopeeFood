using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using ShopeeFood_Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_Web
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
            services.AddControllersWithViews();

            services.AddDistributedMemoryCache();

            services.AddSession();

            services.AddControllers();

            services.AddMvc();

            services.AddOptions();
            var mailSettings = Configuration.GetSection("MailSettings");

            services.Configure<MailSettings>(mailSettings);

            services.AddTransient<SendMailService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSession();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=HomePageShopeeFood}/{id?}");

                //endpoints.MapGet("/TestSendMail", async context =>
                //{
                //    var message = await Controllers.CustomerController.SendMail("phamtuandat16072001@gmail.com",
                //    "lethithanhthuy11102001@gmail.com", "TEST SEND MAIL", ".................", "phamtuandat16072001@gmail.com", "@");

                //    await context.Response.WriteAsync(message);
                //});

                endpoints.MapGet("/TestSendMailService", async context =>
                {
                    var sendMailService = context.RequestServices.GetService<SendMailService>();

                    var mailContent = new MailContent();
                    mailContent.To = "dat16072001@gmail.com";
                    mailContent.Title = "TEST MAIL SERVICE";
                    mailContent.Content = "TEST THANH CONG";

                    var result = await sendMailService.SendMail(mailContent);

                    await context.Response.WriteAsync(result);
                });
            });
        }
    }
}
