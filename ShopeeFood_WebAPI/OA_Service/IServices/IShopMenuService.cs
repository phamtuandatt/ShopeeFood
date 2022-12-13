using OA_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Service.IServices
{
    public interface IShopMenuService
    {
        IEnumerable<ShopMenu> GetAll();

        ShopMenu Get(int Id);

        void Insert(ShopMenu Entity);

        void Delete(int Id);

        void Update(ShopMenu Entity);

        void SaveChange();

        IEnumerable<ShopMenu> GetProductTypeShop(int shopId);
    }
}
