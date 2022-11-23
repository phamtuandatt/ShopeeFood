using OA_Data.Entities;
using OA_Repository.IRepository;
using OA_Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Service.Services
{
    public class ShopMenuService : IShopMenuService
    {
        private IRepository<ShopMenu> _shopMenuRepository;

        public ShopMenuService(IRepository<ShopMenu> shopMenuRepository)
        {
            _shopMenuRepository = shopMenuRepository;
        }

        public void Delete(int Id)
        {
            _shopMenuRepository.Delete(Id);
        }

        public ShopMenu Get(int Id)
        {
            return _shopMenuRepository.Get(Id);
        }

        public IEnumerable<ShopMenu> GetAll()
        {
            return _shopMenuRepository.GetAll();
        }

        public void Insert(ShopMenu Entity)
        {
            _shopMenuRepository.Insert(Entity);
        }

        public void SaveChange()
        {
            _shopMenuRepository.SaveChange();
        }

        public void Update(ShopMenu Entity)
        {
            _shopMenuRepository.Update(Entity);
        }
    }
}
