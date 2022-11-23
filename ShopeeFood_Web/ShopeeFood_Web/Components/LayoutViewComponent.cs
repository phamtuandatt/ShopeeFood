using Microsoft.AspNetCore.Mvc;
using ShopeeFood_Web.Models;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System;
using System.Threading.Tasks;

namespace ShopeeFood_Web.Components
{
    public class LayoutViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            using (var client = new HttpClient())
            {
                // Get link of API
                client.BaseAddress = new Uri("https://localhost:5001/");

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // Access API
                HttpResponseMessage response = await client.GetAsync("api/City");

                if (response.IsSuccessStatusCode)
                {
                    // Read data
                    var details = response.Content.ReadAsAsync<IEnumerable<CityModel>>().Result;

                    return View("InvokeAsync", details);
                }
            }
            return View();
        }
    }
}
