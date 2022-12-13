using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OA_Data.Entities;
using OA_Service.IServices;
using OA_Service.Services;
using ShopeeFood_WebAPI.Models;
using System.Collections.Generic;

namespace ShopeeFood_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopMenuController : ControllerBase
    {
        private readonly IShopMenuService shopMenuService;
        private readonly IProductService productService;
        private readonly IProductTypeService productTypeService;

        public ShopMenuController(IShopMenuService shopMenuService, IProductService productService, IProductTypeService productTypeService)
        {
            this.shopMenuService = shopMenuService;
            this.productService = productService;
            this.productTypeService = productTypeService;
        }

        [HttpGet("GetShopMenus")]
        public IEnumerable<ShopMenu> GetShopMenus()
        {
            return shopMenuService.GetAll();
        }

        // Get ShopMenu
        [HttpGet("GetShopMenu")]
        public IEnumerable<ShopMenuModel> GetShopMenu(int shopId)
        {
            List<ShopMenuModel> shopMenuModels = new List<ShopMenuModel>();
            var lst_Menus = GetShopMenus();
            foreach (var item in lst_Menus)
            {
                if (item.ShopId == shopId)
                {
                    var product = productService.Get(item.ProductId);
                    ShopMenuModel model = new ShopMenuModel();
                    model.ShopId = item.ShopId;
                    model.ProductId = item.ProductId;
                    model.ProductTypeId = item.ProductTypeId;
                    model.ProductName = product.ProductName;
                    model.Cost = product.Cost;
                    model.Discount = product.Discount;
                    model.Price = product.Price;
                    model.ProductImage = product.ProductImage;
                    model.ProductDescription = product.ProductDescription;

                    shopMenuModels.Add(model);
                }
            }

            return shopMenuModels;
        }
    }
}

